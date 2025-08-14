FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 👇 copiar SOLO csproj por capas (rutas DESDE la raíz del repo)
COPY src/Services/AccountService/AccountService.API/AccountService.API.csproj src/Services/AccountService/AccountService.API/
COPY src/Services/AccountService/AccountService.Application/AccountService.Application.csproj src/Services/AccountService/AccountService.Application/
COPY src/Services/AccountService/AccountService.Domain/AccountService.Domain.csproj src/Services/AccountService/AccountService.Domain/
COPY src/Services/AccountService/AccountService.Infrastructure/AccountService.Infrastructure.csproj src/Services/AccountService/IdentityService.Infrastructure/

RUN dotnet restore src/Services/AccountService/AccountService.API/AccountService.API.csproj

# 👇 ahora sí, el resto del código
COPY . .
WORKDIR /src/src/Services/AccountService/AccountService.API
RUN dotnet publish AccountService.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AccountService.API.dll"]
