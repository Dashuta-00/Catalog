# 🏗️ Checkout Project - Clean Architecture

Микросервис построен по принципам Clean Architecture с четким разделением слоев и зависимостей

## 📋 Структура проектов

| Проект | Назначение | Зависимости |
|--------|------------|-------------|
| **Checkout.API** | Web API, контроллеры, точка входа | Application, Infrastructure |
| **Checkout.Application** | Бизнес-логика, сервисы, use cases | Domain |
| **Checkout.Domain** | Доменные модели, интерфейсы, entities | - |
| **Checkout.Infrastructure** | Данные, внешние сервисы, repositories | Application |


## ⚡ Быстрая настройка

### Добавить все проекты в solution

```bash
dotnet sln add src/Services/Checkout/**/*.csproj
```

### Или добавить проекты по отдельности

```bash
# API-слой (зависит от Application, Infrastructure)
dotnet sln add "src/Services/Checkout/Checkout.API/Checkout.API.csproj"

# Слой приложения (зависит от Domain)
dotnet sln add "src/Services/Checkout/Checkout.Application/Checkout.Application.csproj"

# Доменный слой (без зависимостей)
dotnet sln add "src/Services/Checkout/Checkout.Domain/Checkout.Domain.csproj"

# Инфраструктурный слой (зависит от Application)
dotnet sln add "src/Services/Checkout/Checkout.Infrastructure/Checkout.Infrastructure.csproj"
```

## 🚀 Сборка и запуск

```bash
# Восстановить зависимости
dotnet restore

# Собрать проект
dotnet build

# Запустить API
dotnet run --project src/Services/Checkout/Checkout.API
```

## 🔍 Проверка настройки

```bash
# Просмотреть все проекты в solution
dotnet sln list

# Проверить зависимости проектов
dotnet list src/Services/Checkout/Checkout.API reference
```

## 🎯 Архитектурные принципы

### Инверсия зависимостей
> Domain не зависит ни от чего.

### Разделение ответственности
> Каждый слой имеет чёткое назначение, бизнес-логика изолирована от внешних зависимостей.

### Расширяемость
> Легко добавлять новую функциональность.