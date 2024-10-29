FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend-Auth/Sekmen.Commerce.Services.Auth.Api/Sekmen.Commerce.Services.Auth.Api.csproj", "Backend-Auth/Sekmen.Commerce.Services.Auth.Api/"]
COPY ["Backend-Auth/Sekmen.Commerce.Services.Auth.Domain/Sekmen.Commerce.Services.Auth.Domain.csproj", "Backend-Auth/Sekmen.Commerce.Services.Auth.Domain/"]
COPY ["Backend-Auth/Sekmen.Commerce.Services.Auth.Application/Sekmen.Commerce.Services.Auth.Application.csproj", "Backend-Auth/Sekmen.Commerce.Services.Auth.Application/"]
COPY ["Backend-Auth/Sekmen.Commerce.Services.Auth.Infrastructure/Sekmen.Commerce.Services.Auth.Infrastructure.csproj", "Backend-Auth/Sekmen.Commerce.Services.Auth.Infrastructure/"]
RUN dotnet restore "Backend-Auth/Sekmen.Commerce.Services.Auth.Api/Sekmen.Commerce.Services.Auth.Api.csproj"
COPY . .
WORKDIR "/src/Backend-Auth/Sekmen.Commerce.Services.Auth.Api"
RUN dotnet build "Sekmen.Commerce.Services.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sekmen.Commerce.Services.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sekmen.Commerce.Services.Auth.Api.dll"]
