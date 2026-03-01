FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["gateway/ShipTrack.Gateway/ShipTrack.Gateway.csproj", "gateway/ShipTrack.Gateway/"]
RUN dotnet restore "gateway/ShipTrack.Gateway/ShipTrack.Gateway.csproj"
COPY . .
WORKDIR "/src/gateway/ShipTrack.Gateway"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipTrack.Gateway.dll"]

