# Use a imagem SDK para compilação (Stage 1: build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copia o arquivo de solução
COPY Shoop.sln .

# 2. Copia explicitamente as pastas que contêm os arquivos .csproj
# Isso permite que o dotnet restore e o build acessem as referências de projeto.
# Atenção: Ajuste se você tiver mais pastas de projeto que não estão listadas aqui.
COPY Shoop.APi/ Shoop.APi/
COPY Shoop.Application/ Shoop.Application/
COPY Shoop.CrossCutting/ Shoop.CrossCutting/
COPY Shoop.Domain/ Shoop.Domain/
COPY Shoop.Infrastructure/ Shoop.Infrastructure/

# 3. Restaura as dependências
RUN dotnet restore

# 4. Publica a aplicação FINAL (usa o caminho explícito)
# Usamos WORKDIR /src, então o caminho é Shoop.APi/Shoop.APi.csproj
# Não precisamos de um segundo COPY . . pois as pastas já foram copiadas no passo 2
RUN dotnet publish Shoop.APi/Shoop.APi.csproj -c Release -o /app/publish

# Use a imagem de tempo de execução (Stage 2: final)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Define a porta
EXPOSE 8080

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "Shoop.APi.dll"]