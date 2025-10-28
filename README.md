# BookCatalog API

BookCatalog is a small Web API built on .NET 6 that demonstrates a Clean Architecture approach.
It uses an in-memory database (for easy testing and development) and exposes endpoints for
managing books and authors. Swagger UI is included for interactive API exploration.

## Architecture

This repository follows a layered/clean architecture split across four projects:

| Project                      | Role                | Responsibility                                |
| ---------------------------- | ------------------- | --------------------------------------------- |
| `BookCatalog.Domain`         | Core                | Entity models (e.g. `Book`, `Author`)         |
| `BookCatalog.Application`    | Business Logic      | DTOs, service contracts, mappings             |
| `BookCatalog.Infrastructure` | Data Implementation | EF Core In-Memory setup, data access, seeding |
| `BookCatalog.API`            | Presentation        | Controllers and Swagger UI                    |

## Requirements

- .NET 6 SDK

## Quick start

1. Clone the repository:

```powershell
git clone https://github.com/BAINDA/upgaming-giorgi-baindurashvili.git
cd upgaming-giorgi-baindurashvili
```

2. Run the API from the solution root:

```powershell
dotnet run --project BookCatalog.API
```

If you open the `BookCatalog.API` project in Visual Studio you can also run it from the IDE — Swagger will be launched automatically.

By default Swagger (HTTPS) is available at https://localhost:5001 and HTTP at http://localhost:5000 (these are the defaults used by the included launch profiles).

Note: The app uses an in-memory database that is seeded on startup. The seed data is reset every time the app restarts.

## API overview

All endpoints return a standard `ApiResponse` wrapper. The main endpoints are:

Authors (base path: `/api/Authors`)

| Method | Path                | Description         |
| ------ | ------------------- | ------------------- |
| GET    | `/api/Authors`      | Get all authors     |
| GET    | `/api/Authors/{id}` | Get author by id    |
| POST   | `/api/Authors`      | Create a new author |
| PUT    | `/api/Authors/{id}` | Update an author    |
| DELETE | `/api/Authors/{id}` | Delete an author    |

Books (base path: `/api/Books`)

| Method | Path                              | Description                               |
| ------ | --------------------------------- | ----------------------------------------- |
| GET    | `/api/Books`                      | Get all books (supports query parameters) |
| GET    | `/api/Books/{id}`                 | Get a single book by id                   |
| GET    | `/api/Books/by-author/{authorId}` | Get books by a specific author            |
| POST   | `/api/Books`                      | Create a new book                         |
| PUT    | `/api/Books/{id}`                 | Update a book                             |
| DELETE | `/api/Books/{id}`                 | Delete a book                             |

GET /api/Books — supported query parameters

- PublicationYear (int) — filter by exact publication year.
- TitleKeyword (string) — case-insensitive substring match against Title.
- AuthorName (string) — case-insensitive substring match against Author.Name.
- SortBy (string) — supported values:
  - title_asc
  - title_desc
  - year_asc
  - year_desc
  - author_asc
  - author_desc

## Development notes

- The in-memory database and seeding are located in `BookCatalog.Infrastructure/Data`.
- Mappings and DTOs live in `BookCatalog.Application/DTOs` and `Mappings`.

## Database

A Database folder exists at the solution root containing `schema.sql`. Running this script will:

- Create a SQL Server database named `BookCatalogDB` if it does not exist.
- If the database/tables already exist, the script drops and recreates the `Authors` and `Books` tables.
- Insert initial seed data.
