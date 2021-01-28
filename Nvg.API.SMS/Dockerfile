#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Tv2.sln", "Tv2.sln"]

COPY ["Infra/Tv2.Infra/Tv2.Infra.csproj", "Infra/Tv2.Infra/"]
COPY ["Infra/Tv2.TenantDataProvider/Tv2.TenantDataProvider.csproj", "Infra/Tv2.TenantDataProvider/"]
COPY ["Src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "Src/BuildingBlocks/EventBus/EventBusRabbitMQ/"]
COPY ["Src/BuildingBlocks/EventBus/EventBus/EventBus.csproj", "Src/BuildingBlocks/EventBus/EventBus/"]

COPY ["Src/Services/FileUploadService/Tv2.Api.FileUpload/Tv2.Api.FileUpload.csproj", "Src/Services/FileUploadService/Tv2.Api.FileUpload/"]
COPY ["Src/Services/FileUploadService/Tv2.FileUploadService/Tv2.FileUploadService.csproj", "Src/Services/FileUploadService/Tv2.FileUploadService/"]

COPY ["Src/Services/IdentityService/Tv2.IdentityService/Tv2.Identity.csproj", "Src/Services/IdentityService/Tv2.IdentityService/"]

COPY ["Src/Services/BillingService/Tv2.Api.Billing/Tv2.Api.Billing.csproj", "Src/Services/BillingService/Tv2.Api.Billing/"]
COPY ["Src/Services/BillingService/Tv2.BillingService/Tv2.BillingService.csproj", "Src/Services/BillingService/Tv2.BillingService/"]

COPY ["Src/Services/EmailService/Tv2.EmailService/Tv2.EmailService.csproj", "Src/Services/EmailService/Tv2.EmailService/"]
COPY ["Src/Services/EmailService/Tv2.API.EmailService/Tv2.API.EmailService.csproj", "Src/Services/EmailService/Tv2.API.EmailService/"]
COPY ["Src/Services/EmailService/Tv2.EmailBackgroundTask/Tv2.EmailBackgroundTask.csproj", "Src/Services/EmailService/Tv2.EmailBackgroundTask/"]

COPY ["Src/Services/OrgService/OrgService/Tv2.OrgService.csproj", "Src/Services/OrgService/OrgService/"]
COPY ["Src/Services/OrgService/Tv2.Api.All/Tv2.Api.All.csproj", "Src/Services/OrgService/Tv2.Api.All/"]

COPY ["Src/Services/RosterService/Tv2.Api.Roster/Tv2.Api.Roster.csproj", "Src/Services/RosterService/Tv2.Api.Roster/"]
COPY ["Src/Services/RosterService/Tv2.RosterService/Tv2.RosterService.csproj", "Src/Services/RosterService/Tv2.RosterService/"]
COPY ["Src/Services/RosterService/Tv2.RosterBackgroundTask/Tv2.RosterBackgroundTask.csproj", "Src/Services/RosterService/Tv2.RosterBackgroundTask/"]

COPY ["Src/Services/SaasAdministrationService/Tv2.Api.SaasAdministration/Tv2.Api.SaasAdministration.csproj", "Src/Services/SaasAdministrationService/Tv2.Api.SaasAdministration/"]
COPY ["Src/Services/SaasAdministrationService/Tv2.SaasAdministrationService/Tv2.SaasAdministrationService.csproj", "Src/Services/SaasAdministrationService/Tv2.SaasAdministrationService/"]

COPY ["Src/Services/SMSService/Tv2.API.SMSService/Tv2.API.SMSService.csproj", "Src/Services/SMSService/Tv2.API.SMSService/"]
COPY ["Src/Services/SMSService/Tv2.SMSService/Tv2.SMSService.csproj", "Src/Services/SMSService/Tv2.SMSService/"]
COPY ["Src/Services/SMSService/Tv2.SMSBackgroundTask/Tv2.SMSBackgroundTask.csproj", "Src/Services/SMSService/Tv2.SMSBackgroundTask/"]

COPY ["Src/Services/TransportService/Tv2.Api.Transport/Tv2.Api.Transport.csproj", "Src/Services/TransportService/Tv2.Api.Transport/"]
COPY ["Src/Services/TransportService/Tv2.TransportService/Tv2.TransportService.csproj", "Src/Services/TransportService/Tv2.TransportService/"]

COPY ["Src/Services/TripService/Tv2.Api.Trip/Tv2.Api.Trip.csproj", "Src/Services/TripService/Tv2.Api.Trip/"]
COPY ["Src/Services/TripService/Tv2.TripService/Tv2.TripService.csproj", "Src/Services/TripService/Tv2.TripService/"]
COPY ["Src/Services/TripService/Tv2.AdhocTripRequestService/Tv2.AdhocTripRequestService.csproj", "Src/Services/TripService/Tv2.AdhocTripRequestService/"]
COPY ["Src/Services/TripService/Tv2.TripBackgroundTask/Tv2.TripBackgroundTask.csproj", "Src/Services/TripService/Tv2.TripBackgroundTask/"]

COPY ["Src/Services/VendorService/Tv2.Api.Vendor/Tv2.Api.Vendor.csproj", "Src/Services/VendorService/Tv2.Api.Vendor/"]
COPY ["Src/Services/VendorService/Tv2.VendorService/Tv2.VendorService.csproj", "Src/Services/VendorService/Tv2.VendorService/"]

COPY ["Src/ApiGateways/Mobile.Bff.Etms/Mobile.Etms.HttpAggregator/Mobile.Etms.HttpAggregator.csproj", "Src/ApiGateways/Mobile.Bff.Etms/Mobile.Etms.HttpAggregator/"]

COPY ["Src/ApiGateways/Web.Bff.Etms/Aggregator/Web.Etms.HttpAggregator/Web.Etms.HttpAggregator.csproj", "Src/ApiGateways/Web.Bff.Etms/Aggregator/Web.Etms.HttpAggregator/"]

COPY ["Src/Services/WorkspaceService/Tv2.Api.Workspace/Tv2.Api.Workspace.csproj", "Src/Services/WorkspaceService/Tv2.Api.Workspace/"]
COPY ["Src/Services/WorkspaceService/Tv2.WorkspaceService/Tv2.WorkspaceService.csproj", "Src/Services/WorkspaceService/Tv2.WorkspaceService/"]

COPY ["Src/Services/OrgService/Test/Tv2.Test.EmployeeService/Tv2.Test.EmployeeService.csproj", "Src/Services/OrgService/Test/Tv2.Test.EmployeeService/"]

COPY ["Src/Services/RosterService/Test/Tv2.Test.RosterService/Tv2.Test.RosterService.csproj", "Src/Services/RosterService/Test/Tv2.Test.RosterService/"]

COPY ["docker-compose.dcproj", "docker-compose.dcproj"]


RUN dotnet restore "Tv2.sln"

COPY . .
WORKDIR "/src/Src/Services/SMSService/Tv2.API.SMSService"
RUN dotnet build "Tv2.API.SMSService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tv2.API.SMSService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tv2.API.SMSService.dll"]