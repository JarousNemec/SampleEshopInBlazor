FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Eshop.Lib/Eshop.Lib.csproj", "Eshop.Lib/"]
RUN dotnet restore "Eshop.Lib/Eshop.Lib.csproj"
COPY . .
WORKDIR "/src/Eshop.Lib"
RUN dotnet build "Eshop.Lib.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Eshop.Lib.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eshop.Lib.dll"]
