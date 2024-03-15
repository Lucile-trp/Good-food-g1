FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

ENV ConnectionStrings__DefaultConnection="Server=tcp:postgresfranchise,5432;Initial Catalog=Franchise;Persist Security Info=False;User ID=franchisepostgresadmin;Password=admin;Connection Timeout=30;"
ENV RabbitMQ__Hostname=rabbitmq
ENV RabbitMQ__Username=admin
ENV RabbitMQ__Password=admin

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "./Host/Host.csproj"
# Build
RUN dotnet build "./Host/Host.csproj"

# Publish a release
RUN dotnet publish "./Host/Host.csproj" -c Release -o out

RUN dotnet ef database update

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS publish
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Host.dll" ]
