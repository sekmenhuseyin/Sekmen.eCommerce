FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend-Coupon/Sekmen.Commerce.Services.Coupons.Api/Sekmen.Commerce.Services.Coupons.Api.csproj", "Backend-Coupon/Sekmen.Commerce.Services.Coupons.Api/"]
COPY ["Backend-Coupon/Sekmen.Commerce.Services.Coupons.Application/Sekmen.Commerce.Services.Coupons.Application.csproj", "Backend-Coupon/Sekmen.Commerce.Services.Coupons.Application/"]
COPY ["Backend-Coupon/Sekmen.Commerce.Services.Coupons.Infrastructure/Sekmen.Commerce.Services.Coupons.Infrastructure.csproj", "Backend-Coupon/Sekmen.Commerce.Services.Coupons.Infrastructure/"]
COPY ["Backend-Coupon/Sekmen.Commerce.Services.Coupons.Domain/Sekmen.Commerce.Services.Coupons.Domain.csproj", "Backend-Coupon/Sekmen.Commerce.Services.Coupons.Domain/"]
RUN dotnet restore "Backend-Coupon/Sekmen.Commerce.Services.Coupons.Api/Sekmen.Commerce.Services.Coupons.Api.csproj"
COPY . .
WORKDIR "/src/Backend-Coupon/Sekmen.Commerce.Services.Coupons.Api"
RUN dotnet build "Sekmen.Commerce.Services.Coupons.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sekmen.Commerce.Services.Coupons.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sekmen.Commerce.Services.Coupons.Api.dll"]
