FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/OcelotApiGw/OcelotApiGw.csproj", "src/ApiGateways/OcelotApiGw/"]
RUN dotnet restore "src/ApiGateways/OcelotApiGw/OcelotApiGw.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/OcelotApiGw"
RUN dotnet build "OcelotApiGw.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotApiGw.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApiGw.dll"]
