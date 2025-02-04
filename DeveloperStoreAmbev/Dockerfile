# 🔹 Usando imagem oficial do .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:8080"  # Garante que a API escute em todas as interfaces
EXPOSE 8080

# 🔹 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 🔹 Copiar arquivos da solução
COPY *.sln .
COPY DeveloperEvaluation.API/*.csproj DeveloperEvaluation.API/
COPY DeveloperEvaluation.Application/*.csproj DeveloperEvaluation.Application/
COPY DeveloperEvaluation.Domain/*.csproj DeveloperEvaluation.Domain/
COPY DeveloperEvaluation.Infrastructure/*.csproj DeveloperEvaluation.Infrastructure/

# 🔹 Restaurar pacotes NuGet
RUN dotnet restore DeveloperEvaluation.API/DeveloperEvaluation.API.csproj

# 🔹 Copiar código-fonte restante e compilar
COPY . .
WORKDIR /src/DeveloperEvaluation.API
RUN dotnet build -c Release -o /app/build

# 🔹 Publicar aplicação
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# 🔹 Rodar aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 🔹 Adiciona healthcheck para verificar se a API está rodando
HEALTHCHECK --interval=30s --timeout=10s --start-period=10s \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "DeveloperEvaluation.API.dll"]
