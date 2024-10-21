FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sekmen.Commerce.MessageBus/Sekmen.Commerce.MessageBus/Sekmen.Commerce.MessageBus.csproj", "Sekmen.Commerce.MessageBus/Sekmen.Commerce.MessageBus/"]
RUN dotnet restore "Sekmen.Commerce.MessageBus/Sekmen.Commerce.MessageBus/Sekmen.Commerce.MessageBus.csproj"
COPY . .
WORKDIR "/src/Sekmen.Commerce.MessageBus/Sekmen.Commerce.MessageBus"
RUN dotnet build "Sekmen.Commerce.MessageBus.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sekmen.Commerce.MessageBus.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sekmen.Commerce.MessageBus.dll"]
