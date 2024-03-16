FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "./Host/Host.csproj"
# Build
RUN dotnet build "./Host/Host.csproj"

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet ef migrations add init --project "./Host/Host.csproj"

# Publish a release
RUN dotnet publish "./Host/Host.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS publish
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Host.dll" ]

