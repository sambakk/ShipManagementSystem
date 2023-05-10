# Build Stage

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build-env
WORKDIR /source
COPY . .
RUN dotnet restore "./ShipManagement/ShipManagement.Api/ShipManagement.Api.csproj"
RUN dotnet publish "./ShipManagement/ShipManagement.Api/ShipManagement.Api.csproj" -c Release -o /app

# Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:5000
COPY --from=build-env /app ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "ShipManagement.Api.dll"]
