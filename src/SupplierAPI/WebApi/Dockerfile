FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SupplierAPI/SupplierAPI.csproj", "SupplierAPI/"]
RUN dotnet restore "SupplierAPI/SupplierAPI.csproj"

COPY . .
WORKDIR "/src/SupplierAPI"
RUN dotnet build "SupplierAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SupplierAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupplierAPI.dll"]
