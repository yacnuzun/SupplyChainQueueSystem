FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BillApi/BillApi.csproj", "BillApi/"]
RUN dotnet restore "BillApi/BillApi.csproj"

COPY . .
WORKDIR "/src/BillApi"
RUN dotnet build "BillApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BillApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BillApi.dll"]
