FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Inno_Shop.Auth.Service/Inno_Shop.Auth.Service.csproj", "Inno_Shop.Auth.Service/"]
RUN dotnet restore "Inno_Shop.Auth.Service/Inno_Shop.Auth.Service.csproj"
COPY . .
WORKDIR "/src/Inno_Shop.Auth.Service"
RUN dotnet build "Inno_Shop.Auth.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Inno_Shop.Auth.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inno_Shop.Auth.Service.dll"]
