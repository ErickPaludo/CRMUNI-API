FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CRMUNI.UI.Api/CRMUNI.UI.Api.csproj", "CRMUNI.UI.Api/"]
RUN dotnet restore "CRMUNI.UI.Api/CRMUNI.UI.Api.csproj"
COPY . .
WORKDIR "/src/CRMUNI.UI.Api"
RUN dotnet build "./CRMUNI.UI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRMUNI.UI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRMUNI.UI.Api.dll"]
