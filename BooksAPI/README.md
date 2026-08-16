# Books API

RESTful Web API для управления книгами, авторами и жанрами.

Проект разработан на **ASP.NET Core 8** с использованием **Entity Framework Core** и **SQL Server**.

---

## Описание проекта

**Books API** — серверное приложение для работы с библиотекой книг.

API предоставляет возможность:
- получать список книг;
- получать информацию о конкретной книге;
- создавать новые книги;
- обновлять данные книг;
- удалять книги;
- работать с авторами и жанрами.

Проект реализован в рамках изучения Backend-разработки на платформе .NET.

---

## Технологии

- C#
- ASP.NET Core 8 Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- LINQ
- Async/Await
- Data Annotations для валидации моделей

---

## Архитектура проекта

Проект построен по стандартной структуре ASP.NET Core Web API:

```
BooksAPI
│
├── Controllers
│   ├── BooksController.cs
│   ├── AuthorsController.cs
│   └── GenresController.cs
│
├── Models
│   ├── Book.cs
│   ├── Author.cs
│   ├── Genre.cs
│   ├── DTO
│   │   ├── BookDto.cs
│   │   ├── InfoBookDto.cs
│   │   └── AuthorDto.cs
│   └── BookManagementContext.cs
│
├── Database
│   └── BookManagement.sql
│
├── Program.cs
├── appsettings.json
└── README.md
```

---

# База данных

В проекте используется **SQL Server**.

Основные таблицы:

## Books

Хранит информацию о книгах:

- Id
- Title
- Description
- AuthorId
- GenreId
- PublishedYear
- ISBN
- CreatedAt


## Authors

Хранит информацию об авторах:

- Id
- Name
- Biography


## Genres

Хранит информацию о жанрах:

- Id
- Name


Связи между таблицами:

```
Author 1 ---- N Book N ---- 1 Genre
```

Один автор может иметь несколько книг.

Один жанр может содержать несколько книг.

---

# Entity Framework Core

Для работы с базой данных используется **Entity Framework Core**.

Реализовано:

- подключение к SQL Server;
- работа через `DbContext`;
- выполнение CRUD-операций;
- использование LINQ-запросов;
- получение связанных данных через навигационные свойства.

Подключение к базе данных настроено через:

```
appsettings.json
```

Пример:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=SERVER_NAME;Initial Catalog=BookManagement;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

---

# API Endpoints

## Books API

### Получить все книги

```
GET /api/books
```

Возвращает список всех книг.


---

### Получить книгу по идентификатору

```
GET /api/books/{id}
```

Возвращает информацию о конкретной книге.


---

### Создать новую книгу

```
POST /api/books
```

Пример запроса:

```json
{
  "title": "Война и мир",
  "description": "Роман Льва Толстого",
  "author": "Лев Толстой",
  "genre": "Роман",
  "publishedYear": 1869,
  "isbn": "9785170906797"
}
```

При создании книги:

- выполняется поиск автора;
- выполняется поиск жанра;
- если автор или жанр отсутствуют — создаются новые записи;
- устанавливается дата создания книги.


---

### Обновить книгу

```
PUT /api/books/{id}
```

Обновляет информацию о существующей книге.


---

### Удалить книгу

```
DELETE /api/books/{id}
```

Удаляет книгу из базы данных.

---

# Authors API

## Получить всех авторов

```
GET /api/authors
```

Возвращает список всех авторов.


---

## Получить автора по идентификатору

```
GET /api/authors/{id}
```

Возвращает информацию об авторе.


---

## Создать автора

```
POST /api/authors
```

Пример запроса:

```json
{
  "name": "Лев Толстой",
  "biography": "Русский писатель и мыслитель."
}
```

Создаёт нового автора.

---

# Genres API

## Получить все жанры

```
GET /api/genres
```

Возвращает список всех жанров.


---

## Удалить жанр

```
DELETE /api/genres/{id}
```

Удаляет жанр из базы данных.

---

# Валидация данных

В проекте реализована валидация входных моделей с использованием **Data Annotations**.

Проверяются:

- обязательные поля;
- длина строк;
- диапазон значений;
- корректность ISBN.

При отправке некорректных данных API возвращает:

```
400 Bad Request
```

---

# Swagger / OpenAPI

Для тестирования API используется Swagger.

После запуска приложения документация доступна:

```
https://localhost:{port}/swagger
```

Swagger позволяет:

- просматривать доступные endpoints;
- отправлять HTTP-запросы;
- проверять ответы API;
- тестировать работу контроллеров без отдельного клиентского приложения.

---

# Запуск проекта

## Требования

Перед запуском необходимо установить:

- Visual Studio 2022
- .NET 8 SDK
- SQL Server
- SQL Server Management Studio (SSMS)


## Настройка базы данных

Для создания базы данных и необходимых таблиц используется SQL-скрипт:

```text
Database/BookManagement.sql
```

Скрипт содержит:

- создание базы данных
- структуру таблиц `Books`, `Authors` и `Genres`;
- первичные ключи;
- внешние ключи;
- связи между таблицами;
- тестовые данные.

Для настройки базы данных:

1. Открыть SQL Server Management Studio.
2. Подключиться к SQL Server.
3. Открыть файл `Database/BookManagement.sql`.
4. Выполнить SQL-скрипт.
5. Проверить создание таблиц и тестовых данных.

Далее настроить строку подключения в файле:

```
appsettings.json
```


Пример:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=SERVER_NAME;Initial Catalog=BookManagement;Integrated Security=True;TrustServerCertificate=True"
  }
}
```


## Запуск приложения

1. Открыть файл решения:

```
BooksAPI.sln
```

в Visual Studio.

2. Проверить подключение к SQL Server.

3. Запустить проект:

```
F5
```

или:

```
Ctrl + F5
```

4. Открыть Swagger:

```
https://localhost:{port}/swagger
```

---

# HTTP статусы

| Код | Описание |
|---|---|
| 200 OK | Запрос успешно выполнен |
| 201 Created | Ресурс успешно создан |
| 204 No Content | Запрос успешно выполнен без тела ответа |
| 400 Bad Request | Переданы некорректные данные |
| 404 Not Found | Объект не найден |
| 500 Internal Server Error | Ошибка сервера |

---

# Особенности проекта

В проекте реализовано:

- RESTful API для управления библиотекой;
- отдельные контроллеры для книг, авторов и жанров;
- CRUD-операции для книг;
- работа со связанными сущностями;
- использование DTO для передачи данных;
- валидация входных моделей;
- асинхронная работа с базой данных;
- документация API через Swagger/OpenAPI.

---

# Цели проекта

Проект создан для практического изучения Backend-разработки на платформе .NET.

В рамках проекта реализованы:

- разработка REST API;
- работа с Entity Framework Core;
- взаимодействие с SQL Server;
- создание контроллеров;
- проектирование связей между сущностями;
- использование DTO;
- обработка HTTP-запросов и ответов.

---

# Автор

**Иван Хавторин**

Backend Developer (C# / .NET)