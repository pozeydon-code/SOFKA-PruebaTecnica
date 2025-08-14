FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 👇 copiar SOLO csproj por capas (rutas DESDE la raíz del repo)
COPY src/Services/IdentityService/IdentityService.API/IdentityService.API.csproj src/Services/IdentityService/IdentityService.API/
COPY src/Services/IdentityService/IdentityService.Application/IdentityService.Application.csproj src/Services/IdentityService/IdentityService.Application/
COPY src/Services/IdentityService/IdentityService.Domain/IdentityService.Domain.csproj src/Services/IdentityService/IdentityService.Domain/
COPY src/Services/IdentityService/IdentityService.Infrastructure/IdentityService.Infrastructure.csproj src/Services/IdentityService/IdentityService.Infrastructure/

RUN dotnet restore src/Services/IdentityService/IdentityService.API/IdentityService.API.csproj

# 👇 ahora sí, el resto del código
COPY . .
WORKDIR /src/src/Services/IdentityService/IdentityService.API
RUN dotnet publish IdentityService.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.API.dll"]
