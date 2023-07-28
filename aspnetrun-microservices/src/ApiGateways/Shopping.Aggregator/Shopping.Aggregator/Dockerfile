FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/Shopping.Aggregator/Shopping.Aggregator/Shopping.Aggregator.csproj", "src/ApiGateways/Shopping.Aggregator/Shopping.Aggregator/"]
RUN dotnet restore "src/ApiGateways/Shopping.Aggregator/Shopping.Aggregator/Shopping.Aggregator.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/Shopping.Aggregator/Shopping.Aggregator"
RUN dotnet build "Shopping.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Aggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Aggregator.dll"]
