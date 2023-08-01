#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Etapa de construccion, use la imagen base, expone la aplicacion en los puertos 80 y 443
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa restauracion dependencias del proyecto 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY CustomerApp.csproj .
RUN dotnet restore

# Copia el codigo fuente y construye la aplicacion
COPY . .
WORKDIR "/src"
RUN dotnet build "CustomerApp.csproj" -c Release -o /app/build

# Genera un build en modo release en el folder
FROM build AS publish
RUN dotnet publish "CustomerApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Inicia la aplicacion
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerApp.dll"]