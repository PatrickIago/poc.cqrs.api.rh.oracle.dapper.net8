FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5076

ENV ASPNETCORE_URLS=http://+:5076
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/poc.cqrs.api.rh.oracle.dapper.net8/src/01-Presentation

# Copia o arquivo .csproj para o diretório de trabalho atual no contêiner
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/01-Presentation/Poc.RH.API/Poc.RH.API.csproj .
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/02-Application/Poc.Command/Poc.Command.csproj .
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/03-Domain/Poc.Domain/Poc.Domain.csproj .
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/02-Application/Poc.Query/Poc.Query.csproj .
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/02-Application/Poc.Contract/Poc.Contract.csproj .
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/04-Infrastructure/Database/Poc.Oracle/Poc.Oracle.csproj .
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/04-Infrastructure/DistributedCache/Poc.DistributedCache/Poc.DistributedCache.csproj .
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/04-Infrastructure/Integration/Poc.RabbiMQ/Poc.RabbiMQ.csproj .
COPY src/poc.cqrs.api.rh.oracle.dapper.net8/src/04-Infrastructure/Integration/Poc.Auth/Poc.Auth.csproj .

# Restaura as dependências do projeto
RUN dotnet restore Poc.RH.API.csproj

# Copia o restante dos arquivos do projeto para o contêiner
COPY . .

# Define o diretório de trabalho e constrói o projeto
WORKDIR /src/poc.cqrs.api.rh.oracle.dapper.net8/src/01-Presentation
RUN dotnet build src/poc.cqrs.api.rh.oracle.dapper.net8/src/01-Presentation/Poc.RH.API/Poc.RH.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish src/poc.cqrs.api.rh.oracle.dapper.net8/src/01-Presentation/Poc.RH.API/Poc.RH.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poc.RH.API.dll"]