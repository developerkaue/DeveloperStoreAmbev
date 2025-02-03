# 🔹 Usando imagem oficial do .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 🔹 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 🔹 Copiar arquivos da solução
COPY ["DeveloperEvaluation.API/DeveloperEvaluation.API.csproj", "DeveloperEvaluation.API/"]
COPY ["DeveloperEvaluation.Application/DeveloperEvaluation.Application.csproj", "DeveloperEvaluation.Application/"]
COPY ["DeveloperEvaluation.Domain/DeveloperEvaluation.Domain.csproj", "DeveloperEvaluation.Domain/"]
COPY ["DeveloperEvaluation.Infrastructure/DeveloperEvaluation.Infrastructure.csproj", "DeveloperEvaluation.Infrastructure/"]

# 🔹 Restaurar pacotes NuGet
RUN dotnet restore "DeveloperEvaluation.API/DeveloperEvaluation.API.csproj"

# 🔹 Copiar código-fonte restante e compilar
COPY . .
WORKDIR "DeveloperEvaluation.API"
RUN dotnet build -c Release -o /app/build

# 🔹 Publicar aplicação
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# 🔹 Rodar aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeveloperEvaluation.API.dll"]
