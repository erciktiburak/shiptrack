FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["services/ShipTrack.OrderService/ShipTrack.OrderService.csproj", "services/ShipTrack.OrderService/"]
COPY ["shared/ShipTrack.Shared/ShipTrack.Shared.csproj", "shared/ShipTrack.Shared/"]
RUN dotnet restore "services/ShipTrack.OrderService/ShipTrack.OrderService.csproj"
COPY . .
WORKDIR "/src/services/ShipTrack.OrderService"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipTrack.OrderService.dll"]

