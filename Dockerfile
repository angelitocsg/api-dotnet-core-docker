FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /app

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -o dist

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS runtime
ENV ASPNETCORE_URLS http://*:80

WORKDIR /app
COPY --from=build /app/dist .

ENTRYPOINT ["dotnet", "API_Docker.dll"]