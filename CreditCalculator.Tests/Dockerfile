# Базовый образ (SDK для сборки и запуска тестов)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS test
WORKDIR /src

# Копируем проекты для восстановления зависимостей
COPY ["CreditCalculator/CreditCalculator.csproj", "CreditCalculator/"]
COPY ["CreditCalculator.Tests/CreditCalculator.Tests.csproj", "CreditCalculator.Tests/"]

# Восстановление зависимостей
RUN dotnet restore "CreditCalculator.Tests/CreditCalculator.Tests.csproj"

# Копируем все остальные файлы
COPY . .

# Сборка проекта тестов
WORKDIR "/src/CreditCalculator.Tests"
RUN dotnet build -c Release --no-restore

# Запуск тестов
RUN dotnet test -c Release --no-build --logger:"console;verbosity=normal"
