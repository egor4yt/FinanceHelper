FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

RUN apt-get update && apt-get install -y curl

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FinanceHelper.TelegramBot.Api/FinanceHelper.TelegramBot.Api.csproj", "FinanceHelper.TelegramBot.Api/"]
COPY ["FinanceHelper.TelegramBot.Application/FinanceHelper.TelegramBot.Application.csproj", "FinanceHelper.TelegramBot.Application/"]
COPY ["FinanceHelper.TelegramBot.Shared/FinanceHelper.TelegramBot.Shared.csproj", "FinanceHelper.TelegramBot.Shared/"]
COPY ["FinanceHelper.TelegramBot.MessageBroker/FinanceHelper.TelegramBot.MessageBroker.csproj", "FinanceHelper.TelegramBot.MessageBroker/"]
RUN dotnet restore "FinanceHelper.TelegramBot.Api/FinanceHelper.TelegramBot.Api.csproj"
COPY . .
WORKDIR "/src/FinanceHelper.TelegramBot.Api"
RUN dotnet build "FinanceHelper.TelegramBot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinanceHelper.TelegramBot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN rm appsettings.json
RUN mv appsettings.Docker.json appsettings.json
ENTRYPOINT ["dotnet", "FinanceHelper.TelegramBot.Api.dll"]
