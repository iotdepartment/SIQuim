# Imagen base de .NET 9 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo y compilar
COPY . .
RUN dotnet publish -c Release -o /app

# Imagen base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Exponer el puerto
EXPOSE 8080
ENTRYPOINT ["dotnet", "SIQuim.dll"]