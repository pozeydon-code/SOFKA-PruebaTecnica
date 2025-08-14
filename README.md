# 1) Construir imágenes (¡primero!)

```bash
docker compose build
```

# 2) Levantar contenedores

```bash
docker compose up -d
```

Los puertos se manipulan en el docker-compose.yml. Actualmente usan 3001 y el 3002;

# Requisitos

- Docker Desktop (Compose)
- Puertos libres: (3001, 3002, 5003)

# Servicios

- sqlserver: SQL Server 2022; port: 5003 -> 1433
- identity-api: Microservicio 1; port: 3002 -> 8080
- acount-api: Microservicio 1; port: 3001 -> 8080

Las API usan la cadena conexion que se encuenta en el archivo `docker-compose.yml`

```json
Server=sqlserver;Database=banking;User Id=sa;Password=Your_password123;TrustServerCertificate=true.
```

# Estructura Proyecto

```
├─ docker-compose.yml
├─ BaseDatos.sql
└─ src/
  └─ Services/
    ├─ IdentityService/IdentityService.API/Dockerfile
    └─ AccountService/AccountService.API/Dockerfile
```
