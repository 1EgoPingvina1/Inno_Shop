# 🧪 Inno_Shop

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Version](https://img.shields.io/badge/version-1.0.0-brightgreen.svg)](VERSION)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](BUILD)
[![Docker](https://img.shields.io/badge/docker-ready-blue.svg)](DOCKER)
[![CI/CD](https://img.shields.io/badge/CI--CD-GitHub%20Actions-brightgreen.svg)](CI-CD)

---

## 🚀 Описание

`Inno_Shop` — это тренировочный проект, созданный для улучшения навыков работы с современными технологиями разработки веб-приложений. Основной акцент сделан на изучение .NET стека, контейнеризации и CI/CD процессов.

---

## 🛠 Технологии

**Основной стек:**
  
- **Язык программирования**:  
  ![C#](https://img.shields.io/badge/-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) (.NET 7)

- **Базы данных**:  
  ![MS SQL Server](https://img.shields.io/badge/-MS%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)  
  ![PostgreSQL](https://img.shields.io/badge/-PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)

---

**Библиотеки и инструменты:**

- **Entity Framework**:  
  ![EF Core](https://img.shields.io/badge/-Entity%20Framework-512BD4?style=for-the-badge&logo=ef&logoColor=white) — ORM для работы с БД
- **FluentValidation**:  
  ![FluentValidation](https://img.shields.io/badge/-FluentValidation-6DB33F?style=for-the-badge&logo=fluentvalidation&logoColor=white) — для валидации данных
- **MediatR**:  
  ![MediatR](https://img.shields.io/badge/-MediatR-3A86FF?style=for-the-badge&logo=mediatr&logoColor=white) — для обработки запросов/уведомлений
- **Swagger**:  
  ![Swagger](https://img.shields.io/badge/-Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=white) — для документирования API
- **AutoMapper**:  
  ![AutoMapper](https://img.shields.io/badge/-AutoMapper-DD0031?style=for-the-badge&logo=automapper&logoColor=white) — для маппинга объектов

---

**Тестирование:**

- **xUnit**:  
  ![xUnit](https://img.shields.io/badge/-xUnit.net-E23122?style=for-the-badge&logo=xunit&logoColor=white) — фреймворк для модульного тестирования

---

**Сборка и развертывание:**

- **Docker**:  
  ![Docker](https://img.shields.io/badge/-Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white) — для контейнеризации приложения

- **CI/CD**:  
  ![GitHub Actions](https://img.shields.io/badge/-GitHub%20Actions-2088FF?style=for-the-badge&logo=github-actions&logoColor=white) — для автоматизации сборки, тестирования и развертывания

---

## ⚙️ Установка и запуск

1. **Клонируйте репозиторий:**
   ```bash
   git clone https://github.com/username/inno_shop.git

2. **Настройка Docker:**
   ```bash
   docker-compose up --build

3. **Запуск проекта**
   После успешного запуска контейнеров, приложение будет доступно по адресу http:localhost:7017

4. **Тестирование**
  Чтобы запустить модульные тесты:
   ```bash
   dotnet test
