#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Softplan.CalculaJuros.Api/Softplan.CalculaJuros.Api.csproj", "src/Softplan.CalculaJuros.Api/"]
COPY ["src/Softplan.CalculaJuros.ApplicationCore/Softplan.CalculaJuros.ApplicationCore.csproj", "src/Softplan.CalculaJuros.ApplicationCore/"]
COPY ["src/Softplan.CalculaJuros.Infra/Softplan.CalculaJuros.Infra.csproj", "src/Softplan.CalculaJuros.Infra/"]
COPY ["src/Softplan.Commons.Resilience/Softplan.Commons.Resilience.csproj", "src/Softplan.Commons.Resilience/"]
RUN dotnet restore "src/Softplan.CalculaJuros.Api/Softplan.CalculaJuros.Api.csproj"
COPY . .
WORKDIR "/src/src/Softplan.CalculaJuros.Api"
RUN dotnet build "Softplan.CalculaJuros.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Softplan.CalculaJuros.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Softplan.CalculaJuros.Api.dll"]
