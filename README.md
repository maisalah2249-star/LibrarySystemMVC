\# Library System (MVC)



ASP.NET Core MVC (.NET 10) — 3-layer architecture.



\## Structure

\- \*\*LibrarySystem.DAL\*\*: Entities, DbContext (Identity + EF Core), Migrations

\- \*\*LibrarySystem.BLL\*\*: Generic Repository, Unit of Work, Services, Result \& PaginatedList

\- \*\*LibrarySystem.PL\*\*: Controllers, Views, ViewModels, AutoMapper profiles



\## Features

\- CRUD for Authors and Books

\- Search, pagination, image upload

\- Soft delete with global query filters



\## Run

1\. Update `DefaultConnection` in `LibrarySystem.PL/appsettings.json` if needed

2\. `dotnet ef database update --project LibrarySystem.DAL --startup-project LibrarySystem.PL`

3\. Run `LibrarySystem.PL`



\## Screenshots

See the `Screenshots` folder.

