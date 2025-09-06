#!/bin/bash
set -e
# Find the first .csproj in the project
PROJECT_FILE=$(find . -maxdepth 2 -name "*.csproj" | head -n 1)

if [ -z "$PROJECT_FILE" ]; then
  echo ".csproj file not found!"
  exit 1
fi

echo "Restoring $PROJECT_FILE..."
dotnet restore "$PROJECT_FILE"

echo "Building & Publishing..."
dotnet publish "$PROJECT_FILE" -c Release -o /app/publish

echo "Running app..."
exec dotnet /app/publish/*.dll