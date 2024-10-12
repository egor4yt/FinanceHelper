FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FinanceHelper.Api/FinanceHelper.Api.csproj", "FinanceHelper.Api/"]
COPY ["FinanceHelper.Application/FinanceHelper.Application.csproj", "FinanceHelper.Application/"]
COPY ["FinanceHelper.Shared/FinanceHelper.Shared.csproj", "FinanceHelper.Shared/"]
COPY ["FinanceHelper.Persistence/FinanceHelper.Persistence.csproj", "FinanceHelper.Persistence/"]
COPY ["FinanceHelper.Domain/FinanceHelper.Domain.csproj", "FinanceHelper.Domain/"]
RUN dotnet restore "FinanceHelper.Api/FinanceHelper.Api.csproj"
COPY . .
WORKDIR "/src/FinanceHelper.Api"
RUN dotnet build "FinanceHelper.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinanceHelper.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN rm appsettings.json
RUN mv appsettings.Docker.json appsettings.json
ENTRYPOINT ["dotnet", "FinanceHelper.Api.dll"]
