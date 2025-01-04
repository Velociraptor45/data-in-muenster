FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

################## BUILD ##################
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "MuensterData.WebApp/MuensterData.WebApp.csproj"
COPY . .
RUN dotnet build "MuensterData.WebApp/MuensterData.WebApp.csproj" -c Release -o /app/build

################# PUBLISH #################
FROM build AS publish
RUN dotnet publish "MuensterData.WebApp/MuensterData.WebApp.csproj" -c Release -o /app/publish --no-restore --sc false

################## FINAL ##################
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MuensterData.WebApp.dll"]