#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Nvg.SMSBackgroundTask/Nvg.SMSBackgroundTask.csproj", "Nvg.SMSBackgroundTask/"]
COPY ["BuildingBlocks/EventBus/EventBus/EventBus.csproj", "BuildingBlocks/EventBus/EventBus/"]
COPY ["Nvg.SMSService/Nvg.SMSService.csproj", "Nvg.SMSService/"]
COPY ["BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "BuildingBlocks/EventBus/EventBusRabbitMQ/"]
RUN dotnet restore "Nvg.SMSBackgroundTask/Nvg.SMSBackgroundTask.csproj"
COPY . .
WORKDIR "/src/Nvg.SMSBackgroundTask"
RUN dotnet build "Nvg.SMSBackgroundTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nvg.SMSBackgroundTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nvg.SMSBackgroundTask.dll"]