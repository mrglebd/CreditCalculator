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

## Запуск проекта

### Требования
- .NET 6.0 или новее
- Docker (опционально, если хотите запускать через контейнер)

### Сборка и запуск

```sh
# Восстановление зависимостей
 dotnet restore CreditCalculator/CreditCalculator.csproj

# Сборка проекта
 dotnet build CreditCalculator/CreditCalculator.csproj

# Запуск в режиме разработки с hot-reload
 dotnet watch --project CreditCalculator run

# Запуск в Production-сборке
 dotnet run --project CreditCalculator
```

### Запуск через Docker

```sh
# Сборка Docker-образа
docker build -t credit-calculator ./CreditCalculator

# Запуск контейнера (проброс порта 8080 -> 5000)
docker run -p 5000:8080 credit-calculator

# Открыть в браузере:
http://localhost:5000

### (Опционально) Запуск через Docker Compose
```

### Запуск через Docker Compose

```sh
# Сборка и запуск контейнера
docker-compose up --build

# Приложение будет доступно по адресу:
http://localhost:8181
```

## Структура каталогов

- `Controllers/` — контроллеры MVC
- `Logic/` — бизнес-логика (интерфейсы и сервисы)
- `Models/` — модели и DTO
- `Views/` — Razor Views
- `wwwroot/` — статические файлы (CSS, JS)

## Авторы

- Dunyushkin Gleb