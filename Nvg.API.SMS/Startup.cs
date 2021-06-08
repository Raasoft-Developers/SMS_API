using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nvg.API.SMS.AutofacModules;
using Nvg.API.SMS.Filters;
using Nvg.API.SMS.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Nvg.API.SMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureIdentityServer(IServiceCollection services)
        {
            var builder = services.AddAuthentication();
            builder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => SetJwtBearerOptions(options));

            var additionalIdentityUrls = Configuration.GetSection("AdditionalIdentityUrls")?.Value?.Split(",");
            var additionalBearers = new List<string>();
            additionalBearers.Add(JwtBearerDefaults.AuthenticationScheme);
            int counter = 0;
            if (additionalIdentityUrls != null)
            {
                foreach (var additionalIdentityUrl in additionalIdentityUrls)
                {
                    counter++;
                    builder.AddJwtBearer("AdditionalBearer" + counter, options => SetAdditionalJwtBearerOptions(options, additionalIdentityUrl));
                    additionalBearers.Add("AdditionalBearer" + counter);
                }
            }
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(additionalBearers.ToArray())
                    .Build();
            });
        }

        private void SetJwtBearerOptions(JwtBearerOptions options)
        {
            options.Authority = Configuration.GetValue("IdentityUrl", string.Empty);
            options.Audience = Configuration.GetValue("IdentityAudience", string.Empty);
            options.RequireHttpsMetadata = false;
            //Commenting this section or setting ValidateAudience to true gives error, need to check
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.ContentType = "text/plain";
                    return c.Response.WriteAsync("Not authorized.: " + c.Exception.ToString());
                }
            };
        }

        private void SetAdditionalJwtBearerOptions(JwtBearerOptions options, string additionalIdentityUrl)
        {
            options.Authority = additionalIdentityUrl;
            options.Audience = Configuration.GetValue("IdentityAudience", string.Empty);
            options.RequireHttpsMetadata = false;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureIdentityServer(services);
            services.RegisterEventBus(Configuration);
            //services.ConfigureAutoMapper();
            services.AddSMSService(Program.AppName, Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                    //.AllowCredentials();
                });

            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SMS API",
                    Description = "SMS API Swagger.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Placeholder Name",
                        Email = "Placeholder@gmail.com",
                        Url = new Uri("https://example.com/user"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: `Bearer 12345abcdef`",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                         },
                        new List<string>()
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // Filter to modify what to display in the browser when using swagger.
                //c.DocumentFilter<OpenApiCustomDocumentFilter>();
                c.OperationFilter<OpenApiCustomOperationFilter>();
            });
            var container = new ContainerBuilder();
            container.RegisterModule(new ApplicationModule());
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();

            //// specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            app.UseSwagger(c => {
                c.RouteTemplate = "sms/swagger/{documentname}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/sms/swagger/v1/swagger.json", "SMS API V1"); //remote
                c.RoutePrefix = "sms/swagger";
            });

            app.UseRouting();
            app.UseCors("VueCorsPolicy");
            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
