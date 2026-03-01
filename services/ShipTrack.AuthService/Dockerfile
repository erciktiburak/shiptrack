FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["services/ShipTrack.AuthService/ShipTrack.AuthService.csproj", "services/ShipTrack.AuthService/"]
COPY ["shared/ShipTrack.Shared/ShipTrack.Shared.csproj", "shared/ShipTrack.Shared/"]
RUN dotnet restore "services/ShipTrack.AuthService/ShipTrack.AuthService.csproj"
COPY . .
WORKDIR "/src/services/ShipTrack.AuthService"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipTrack.AuthService.dll"]

