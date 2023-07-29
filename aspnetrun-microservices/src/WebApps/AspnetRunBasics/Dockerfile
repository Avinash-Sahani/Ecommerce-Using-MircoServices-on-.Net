FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["../../../Downloads/AspnetMicroservices-main/AspnetMicroservices-main/src/WebApps/AspnetRunBasics/AspnetRunBasics.csproj", "../../../Downloads/AspnetMicroservices-main/AspnetMicroservices-main/src/WebApps/AspnetRunBasics/"]
RUN dotnet restore "../../../Downloads/AspnetMicroservices-main/AspnetMicroservices-main/src/WebApps/AspnetRunBasics/AspnetRunBasics.csproj"
COPY . .
WORKDIR "/src/../../../Downloads/AspnetMicroservices-main/AspnetMicroservices-main/src/WebApps/AspnetRunBasics"
RUN dotnet build "AspnetRunBasics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspnetRunBasics.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspnetRunBasics.dll"]
