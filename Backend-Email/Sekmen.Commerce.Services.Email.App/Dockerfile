FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend-Email/Sekmen.Commerce.Services.Email.App/Sekmen.Commerce.Services.Email.App.csproj", "Backend-Email/Sekmen.Commerce.Services.Email.App/"]
RUN dotnet restore "Backend-Email/Sekmen.Commerce.Services.Email.App/Sekmen.Commerce.Services.Email.App.csproj"
COPY . .
WORKDIR "/src/Backend-Email/Sekmen.Commerce.Services.Email.App"
RUN dotnet build "Sekmen.Commerce.Services.Email.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sekmen.Commerce.Services.Email.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sekmen.Commerce.Services.Email.App.dll"]
