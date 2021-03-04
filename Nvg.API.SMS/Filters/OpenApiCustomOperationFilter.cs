using Microsoft.AspNetCore.Mvc.Routing;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nvg.API.SMS.Filters
{
    public class OpenApiCustomOperationFilter //: IOperationFilter
    {
        //const string routeParameter = "routeParameter";

        //public void Apply(OpenApiOperation openApiOperation, OperationFilterContext context)
        //{
        //    // Get parameters from HttpAttribute. For example [HttpGet("{param1}/{param2}")]. The code in this class doesn't work with [Route] attribute.
        //    // Make sure HttpAttribute parameter is marked with a ? to mark it as optional if its the requirement. For example if param2 is optional: [HttpGet("{param1}/{param2?}")]
        //    // If all parameters are optional then swagger will throw 404 error.
        //    var httpMethodAttributes = context.MethodInfo.GetCustomAttributes(true).OfType<HttpMethodAttribute>();

        //    if (!string.IsNullOrEmpty(httpMethodAttributes?.FirstOrDefault().Template))
        //    {
        //        // Check if any parameter is having a ?, that is the parameter is optional.
        //        var httpMethodAttributesOptional = httpMethodAttributes?.FirstOrDefault(x => x.Template.Contains("?"));

        //        if (httpMethodAttributesOptional == null)
        //            return;

        //        string regex = $"{{(?<{routeParameter}>\\w+)\\?}}";

        //        var matches = Regex.Matches(httpMethodAttributesOptional.Template, regex);

        //        foreach (Match match in matches)
        //        {
        //            var name = match.Groups[routeParameter].Value;
        //            // If parameter is optional, mark it as such.
        //            var parameter = openApiOperation.Parameters.FirstOrDefault(p => p.In == ParameterLocation.Path && p.Name.Equals(name));
        //            if (parameter != null)
        //            {
        //                parameter.AllowEmptyValue = true;
        //                parameter.Description = "Check \"Send empty value\" checkbox if not assigning data otherwise comma will be passed. " +
        //                    "(Checkbox will become visible after clicking \"Try it out\" button)";
        //                parameter.Required = false;
        //                parameter.Schema.Nullable = true;
        //            }
        //        }
        //    }
        //}
    }
}
