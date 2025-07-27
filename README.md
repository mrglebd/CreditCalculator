# CreditCalculator

CreditCalculator — это веб-приложение для расчета кредитов. Приложение реализовано на ASP.NET Core MVC и предоставляет веб-интерфейс для ввода параметров кредита и получения расчетов.

## Архитектура

Проект разделен на несколько слоев:

- **Controllers**  
  Содержит MVC-контроллеры для обработки HTTP-запросов, связанных с расчетом кредитов.

- **Logic**  
  Содержит бизнес-логику (интерфейсы и сервисы) для расчета кредитов.

- **Models**  
  Содержит модели и DTO, используемые для передачи данных между слоями и отображения результатов.

- **Views**  
  Содержит Razor Views для отображения форм ввода и результатов расчетов.

- **wwwroot**  
  Статические файлы (CSS, JS, библиотеки).

- **CreditCalculator.Tests**  
  Отдельный проект с unit-тестами, покрывающими основную бизнес-логику по расчету графика платежей.

## Запуск проекта

### Требования

- .NET 8.0 SDK
- Docker (опционально, если хотите запускать через контейнер)

---

### Сборка и запуск локально

```sh
# Восстановление зависимостей
dotnet restore

# Сборка проекта
dotnet build

# Запуск веб-приложения
dotnet run --project CreditCalculator
```

Приложение будет доступно по адресу:  
http://localhost:5142 (или порту, указанному в `launchSettings.json`)

---

### Запуск через Docker

```sh
# Сборка Docker-образа
docker build -t credit-calculator ./CreditCalculator

# Запуск контейнера (порт 8080 внутри → 8181 снаружи)
docker run -p 8181:8080 credit-calculator
```

Открыть в браузере:  
http://localhost:8181

---

### Запуск через Docker Compose

```sh
docker-compose up --build
```

Приложение будет доступно по адресу:  
http://localhost:8181

---

## Тестирование

Проект `CreditCalculator.Tests` содержит модульные тесты для проверки корректности расчётов по двум стратегиям:

- `AnnualScheduleCalculator` — ежемесячный аннуитетный график
- `DailyScheduleCalculator` — дневной аннуитетный график с шагом

### Запуск тестов локально

```sh
dotnet test
```

---

### Запуск тестов в Docker

```sh
# Сборка и запуск контейнера с тестами
docker build -t credit-calculator-tests -f CreditCalculator.Tests/Dockerfile .

# Тесты выполнятся на этапе RUN и выведут лог в консоль
```

---

### Запуск через Docker Compose

```sh
# Сборка и запуск только тестов
docker compose --profile test up --build tests
```


## Авторы

- Dunyushkin Gleb
