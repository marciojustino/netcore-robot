FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app
COPY . .
RUN dotnet clean Worker -o /app/output
RUN dotnet restore Worker
RUN dotnet publish Worker -c Release -o /app/output

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/output .

ENTRYPOINT ["dotnet", "Worker.dll"]