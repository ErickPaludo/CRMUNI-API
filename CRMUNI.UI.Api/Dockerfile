FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
# No disco a pasta pode estar como CRMUNI.DOMAIN, mas os csproj referenciam CRMUNI.Domain (Linux diferencia maiusculas)
RUN if [ -d CRMUNI.DOMAIN ] && [ ! -d CRMUNI.Domain ]; then mv CRMUNI.DOMAIN CRMUNI.Domain; fi
RUN dotnet publish CRMUNI.UI.Api/CRMUNI.UI.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CRMUNI.UI.Api.dll"]
