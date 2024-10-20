FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend-Product/Sekmen.Commerce.Services.Product.Api/Sekmen.Commerce.Services.Product.Api.csproj", "Backend-Product/Sekmen.Commerce.Services.Product.Api/"]
COPY ["Backend-Product/Sekmen.Commerce.Services.Product.Application/Sekmen.Commerce.Services.Product.Application.csproj", "Backend-Product/Sekmen.Commerce.Services.Product.Application/"]
COPY ["Backend-Product/Sekmen.Commerce.Services.Product.Infrastructure/Sekmen.Commerce.Services.Product.Infrastructure.csproj", "Backend-Product/Sekmen.Commerce.Services.Product.Infrastructure/"]
COPY ["Backend-Product/Sekmen.Commerce.Services.Product.Domain/Sekmen.Commerce.Services.Product.Domain.csproj", "Backend-Product/Sekmen.Commerce.Services.Product.Domain/"]
RUN dotnet restore "Backend-Product/Sekmen.Commerce.Services.Product.Api/Sekmen.Commerce.Services.Product.Api.csproj"
COPY . .
WORKDIR "/src/Backend-Product/Sekmen.Commerce.Services.Product.Api"
RUN dotnet build "Sekmen.Commerce.Services.Product.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sekmen.Commerce.Services.Product.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sekmen.Commerce.Services.Product.Api.dll"]
