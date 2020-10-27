#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Hcs.Health.Monitor/Hcs.Health.Monitor.csproj", "src/Hcs.Health.Monitor/"]
RUN dotnet restore "src/Hcs.Health.Monitor/Hcs.Health.Monitor.csproj"
COPY . .
WORKDIR "/src/src/Hcs.Health.Monitor"
RUN dotnet build "Hcs.Health.Monitor.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hcs.Health.Monitor.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Harbor.Hcs.Health.Monitor.dll"]