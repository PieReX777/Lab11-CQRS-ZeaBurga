# Etapa 1: Construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY *.sln .
COPY Lab11-ZeaBurga/*.csproj Lab11-ZeaBurga/
COPY Lab11-ZeaBurga.Application/*.csproj Lab11-ZeaBurga.Application/
COPY Lab11-ZeaBurga.Domain/*.csproj Lab11-ZeaBurga.Domain/
COPY Lab11-ZeaBurga.Infrastructure/*.csproj Lab11-ZeaBurga.Infrastructure/
RUN dotnet restore
COPY . .
WORKDIR /src/Lab11-ZeaBurga
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Lab11-ZeaBurga.dll"]