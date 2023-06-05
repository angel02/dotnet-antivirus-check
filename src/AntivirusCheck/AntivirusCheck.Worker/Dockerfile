#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AntivirusCheck.Worker/AntivirusCheck.Worker.csproj", "AntivirusCheck.Worker/"]
RUN dotnet restore "AntivirusCheck.Worker/AntivirusCheck.Worker.csproj"
COPY . .
WORKDIR "/src/AntivirusCheck.Worker"
RUN dotnet build "AntivirusCheck.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AntivirusCheck.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AntivirusCheck.Worker.dll"]