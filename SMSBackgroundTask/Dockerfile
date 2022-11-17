#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SMSBackgroundTask/SMSBackgroundTask.csproj", "SMSBackgroundTask/"]
COPY ["BuildingBlocks/EventBus/EventBus/EventBus.csproj", "BuildingBlocks/EventBus/EventBus/"]
COPY ["SMSService/SMSService.csproj", "SMSService/"]
COPY ["BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "BuildingBlocks/EventBus/EventBusRabbitMQ/"]
RUN dotnet restore "SMSBackgroundTask/SMSBackgroundTask.csproj"
COPY . .
WORKDIR "/src/SMSBackgroundTask"
RUN dotnet build "SMSBackgroundTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SMSBackgroundTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMSBackgroundTask.dll"]