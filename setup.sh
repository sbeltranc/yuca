#!/bin/bash
set -e

echo "Building the solution..."
dotnet build

echo "Applying migrations for AuthDbContext..."
dotnet ef database update --context AuthDbContext --startup-project AuthenticationService/AuthenticationService.csproj --project Shared/Shared.Data/Shared.Data.csproj

echo "Applying migrations for UsersDbContext..."
dotnet ef database update --context UsersDbContext --startup-project AuthenticationService/AuthenticationService.csproj --project Shared/Shared.Data/Shared.Data.csproj

echo "Applying migrations for AuditDbContext..."
dotnet ef database update --context AuditDbContext --startup-project AuthenticationService/AuthenticationService.csproj --project AuditService/AuditService.csproj
