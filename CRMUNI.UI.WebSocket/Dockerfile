FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CRMUNI.UI.WebSocket/CRMUNI.UI.WebSocket.csproj", "CRMUNI.UI.WebSocket/"]
RUN dotnet restore "CRMUNI.UI.WebSocket/CRMUNI.UI.WebSocket.csproj"
COPY . .
WORKDIR "/src/CRMUNI.UI.WebSocket"
RUN dotnet build "./CRMUNI.UI.WebSocket.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRMUNI.UI.WebSocket.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRMUNI.UI.WebSocket.dll"]
