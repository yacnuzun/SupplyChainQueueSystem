FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /MicroServiceApp
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /MicroServiceApp

COPY ["AccountApi/AccountApi.csproj", "AccountApi/"]
RUN dotnet restore "AccountApi/AccountApi.csproj"

COPY . .
WORKDIR "AccountApi"
RUN dotnet build "AccountApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountApi.dll"]
