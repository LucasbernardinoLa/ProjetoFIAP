FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV NUGET_PACKAGES=/root/.nuget/packages

COPY nuget.config ./
COPY *.sln ./
COPY ProjetoFIAP/ProjetoFIAP.Api.csproj ProjetoFIAP/
COPY ProjetoFIAP.Tests/ProjetoFIAP.Tests.csproj ProjetoFIAP.Tests/

RUN dotnet restore ProjetoFIAP.sln

COPY . .

RUN dotnet publish ProjetoFIAP/ProjetoFIAP.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ProjetoFIAP.Api.dll"]