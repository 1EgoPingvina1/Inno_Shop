FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Inno_Shop.Product.API/Inno_Shop.Product.API.csproj", "Inno_Shop.Product.API/"]
COPY ["Inno_Shop.Product.Application/Inno_Shop.Product.Application.csproj", "Inno_Shop.Product.Application/"]
COPY ["Inno_Shop.Product.Domain/Inno_Shop.Product.Domain.csproj", "Inno_Shop.Product.Domain/"]
COPY ["Inno_Shop.Product.Persistence/Inno_Shop.Product.Persistence.csproj", "Inno_Shop.Product.Persistence/"]
RUN dotnet restore "Inno_Shop.Product.API/Inno_Shop.Product.API.csproj"
COPY . .
WORKDIR "/src/Inno_Shop.Product.API"
RUN dotnet build "Inno_Shop.Product.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Inno_Shop.Product.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inno_Shop.Product.API.dll"]
