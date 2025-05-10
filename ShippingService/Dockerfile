FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ShippingService/ShippingService.csproj", "ShippingService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "ShippingService/ShippingService.csproj"

COPY . .
WORKDIR "/src/ShippingService"
RUN dotnet build "ShippingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShippingService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShippingService.dll"] 