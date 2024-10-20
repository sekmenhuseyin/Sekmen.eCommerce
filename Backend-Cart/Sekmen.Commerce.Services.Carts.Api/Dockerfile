FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend-Cart/Sekmen.Commerce.Services.Carts.Api/Sekmen.Commerce.Services.Carts.Api.csproj", "Backend-Cart/Sekmen.Commerce.Services.Carts.Api/"]
COPY ["Backend-Cart/Sekmen.Commerce.Services.Carts.Application/Sekmen.Commerce.Services.Carts.Application.csproj", "Backend-Cart/Sekmen.Commerce.Services.Carts.Application/"]
COPY ["Backend-Cart/Sekmen.Commerce.Services.Carts.Infrastructure/Sekmen.Commerce.Services.Carts.Infrastructure.csproj", "Backend-Cart/Sekmen.Commerce.Services.Carts.Infrastructure/"]
COPY ["Backend-Cart/Sekmen.Commerce.Services.Carts.Domain/Sekmen.Commerce.Services.Carts.Domain.csproj", "Backend-Cart/Sekmen.Commerce.Services.Carts.Domain/"]
RUN dotnet restore "Backend-Cart/Sekmen.Commerce.Services.Carts.Api/Sekmen.Commerce.Services.Carts.Api.csproj"
COPY . .
WORKDIR "/src/Backend-Cart/Sekmen.Commerce.Services.Carts.Api"
RUN dotnet build "Sekmen.Commerce.Services.Carts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sekmen.Commerce.Services.Carts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sekmen.Commerce.Services.Carts.Api.dll"]
