#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Hcs.Health.Service/Hcs.Health.Service.csproj", "src/Hcs.Health.Service/"]
RUN dotnet restore "src/Hcs.Health.Service/Hcs.Health.Service.csproj"
COPY . .
WORKDIR "/src/src/Hcs.Health.Service"
RUN dotnet build "Hcs.Health.Service.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hcs.Health.Service.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Harbor.Hcs.Health.Service.dll"]