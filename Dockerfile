# ---------- BUILD ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar csproj para aprovechar cache de Docker
COPY EventService.Api/EventService.Api.csproj EventService.Api/
COPY EventService.Application/EventService.Application.csproj EventService.Application/
COPY EventService.Domain/EventService.Domain.csproj EventService.Domain/
COPY EventService.Infrastructure/EventService.Infrastructure.csproj EventService.Infrastructure/

# Restaurar dependencias
RUN dotnet restore EventService.Api/EventService.Api.csproj

# Copiar todo el código fuente
COPY . .

# Compilar y publicar
WORKDIR /src/EventService.Api
RUN dotnet publish EventService.Api.csproj -c Release -o /app/publish

# ---------- RUNTIME ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "EventService.Api.dll"]