# Imagen base con el SDK de .NET 8.0 para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos del proyecto y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo el código y compilar la aplicación
COPY . .
RUN dotnet publish -c Release -o /app

# Imagen final con solo el runtime de .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "TodoApi.dll"]