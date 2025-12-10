# 🏗️ Catalog Project - Clean Architecture

Микросервис построен по принципам Clean Architecture с четким разделением слоев и зависимостей

## 📋 Структура проектов

| Проект | Назначение | Зависимости |
|--------|------------|-------------|
| **Catalog.API** | Web API, контроллеры, точка входа | Application, Infrastructure |
| **Catalog.Application** | Бизнес-логика, сервисы, use cases | Domain |
| **Catalog.Domain** | Доменные модели, интерфейсы, entities | - |
| **Catalog.Infrastructure** | Данные, внешние сервисы, repositories | Application |


## ⚡ Быстрая настройка

### Добавить все проекты в solution

```bash
dotnet sln add src/Services/Catalog/**/*.csproj
```

### Или добавить проекты по отдельности

```bash
# API-слой (зависит от Application, Infrastructure)
dotnet sln add "src/Services/Catalog/Catalog.API/Catalog.API.csproj"

# Слой приложения (зависит от Domain)
dotnet sln add "src/Services/Catalog/Catalog.Application/Catalog.Application.csproj"

# Доменный слой (без зависимостей)
dotnet sln add "src/Services/Catalog/Catalog.Domain/Catalog.Domain.csproj"

# Инфраструктурный слой (зависит от Application)
dotnet sln add "src/Services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj"
```

## 🚀 Сборка и запуск

```bash
# Восстановить зависимости
dotnet restore

# Собрать проект
dotnet build

# Запустить API
dotnet run --project src/Services/Catalog/Catalog.API
```

## 🔍 Проверка настройки

```bash
# Просмотреть все проекты в solution
dotnet sln list

# Проверить зависимости проектов
dotnet list src/Services/Catalog/Catalog.API reference
```

## 🎯 Архитектурные принципы

### Инверсия зависимостей
> Domain не зависит ни от чего.

### Разделение ответственности
> Каждый слой имеет чёткое назначение, бизнес-логика изолирована от внешних зависимостей.

### Расширяемость
> Легко добавлять новую функциональность.