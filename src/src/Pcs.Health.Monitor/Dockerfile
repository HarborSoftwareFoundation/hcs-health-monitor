#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Pcs.Health.Monitor/Pcs.Health.Monitor.csproj", "src/Pcs.Health.Monitor/"]
RUN dotnet restore "src/Pcs.Health.Monitor/Pcs.Health.Monitor.csproj"
COPY . .
WORKDIR "/src/src/Pcs.Health.Monitor"
RUN dotnet build "Pcs.Health.Monitor.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pcs.Health.Monitor.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parusnik.Pcs.Health.Monitor.dll"]