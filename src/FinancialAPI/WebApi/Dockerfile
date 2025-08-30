FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FinancialAPI/FinancialAPI.csproj", "FinancialAPI/"]
RUN dotnet restore "FinancialAPI/FinancialAPI.csproj"

COPY . .
WORKDIR "/src/FinancialAPI"
RUN dotnet build "FinancialAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialAPI.dll"]
