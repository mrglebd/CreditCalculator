# Базовый образ для запуска (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Образ для сборки (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем проект
COPY ["CreditCalculator/CreditCalculator.csproj", "CreditCalculator/"]
RUN dotnet restore "CreditCalculator/CreditCalculator.csproj"

# Копируем остальные файлы и публикуем
COPY . .
WORKDIR "/src/CreditCalculator"
RUN dotnet publish -c Release -o /app/publish

# Сборка финального контейнера
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CreditCalculator.dll"]
