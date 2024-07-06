#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

RUN mkdir -p /app

# Instalar dependências necessárias para o ChromeDriver e Google Chrome
USER root
RUN apt-get update && apt-get install -y \
    wget \
    gnupg \
    unzip \
    libglib2.0-0 \
    libnss3 \
    libgconf-2-4 \
    libfontconfig1 \
    libxcb1 \
    libxcomposite1 \
    libxdamage1 \
    libxrandr2 \
    libxtst6 \
    libasound2 \
    libatk1.0-0 \
    libatk-bridge2.0-0 \
    libgtk-3-0

# Baixar e instalar o Google Chrome
RUN wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | apt-key add - && \
    echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google-chrome.list && \
    apt-get update && \
    apt-get install -y google-chrome-stable

USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["OpenAdm.Cep.Api/OpenAdm.Cep.Api.csproj", "OpenAdm.Cep.Api/"]
COPY ["OpenAdm.Cep.Application/OpenAdm.Cep.Application.csproj", "OpenAdm.Cep.Application/"]
COPY ["OpenAdm.Cep.Domain/OpenAdm.Cep.Domain.csproj", "OpenAdm.Cep.Domain/"]
COPY ["OpenAdm.Cep.IoC/OpenAdm.Cep.IoC.csproj", "OpenAdm.Cep.IoC/"]
COPY ["OpenAdm.Cep.Infrastructure/OpenAdm.Cep.Infrastructure.csproj", "OpenAdm.Cep.Infrastructure/"]

#ADD OpenAdm.Cep.Api/Driver/chromedriver.exe app/Driver/

RUN dotnet restore "./OpenAdm.Cep.Api/OpenAdm.Cep.Api.csproj"
COPY . .
WORKDIR "/src/OpenAdm.Cep.Api"
RUN dotnet build "./OpenAdm.Cep.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OpenAdm.Cep.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenAdm.Cep.Api.dll"]