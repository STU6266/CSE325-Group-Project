# Frontend Development Guide

## Getting Started

1. Install the .NET 10 SDK.
2. Clone the GitHub repository.
3. Open the ReadingTracker folder.
4. Run `dotnet restore`.
5. Run `dotnet ef database update`.
6. Run `dotnet run --launch-profile http`.
7. Open http://localhost:5261.

See README.md for more details.

## Working with Books

Use IBookService to work with books.

The service has five methods:

- GetMyBooksAsync()
- GetMyBookAsync(id)
- AddBookAsync(book)
- UpdateBookAsync(book)
- DeleteBookAsync(id)

Frontend pages should use IBookService.
Do not access the database directly.

## Example for Blazor Pages

```razor
@using ReadingTracker.Models
@using ReadingTracker.Services
@inject IBookService BookService

@code {
    private List<Book> books = new();

    protected override async Task OnInitializedAsync()
    {
        books = await BookService.GetMyBooksAsync();
    }
}
```

## Sample Data

MockBookService provides three example books:
- The Hobbit - Finished
- Dune - Reading
- The Hunger Games - Want to Read

It is the **default service in Development**. To use the real SQLite BookService
locally, run from the `ReadingTracker` folder:

```bash
UseMockBookService=false dotnet run --launch-profile http
```

Production always uses the real BookService.

The mock keeps changes in memory.
Changes are not saved to the database.
A new service scope starts with fresh sample data.

The mock does not provide real user security.
The real BookService now implements persistent CRUD and enforces book ownership
using the authenticated server-side user ID; it passed 15 isolated tests.
The new book UI still needs an end-to-end two-account browser test.

For local registration, this starter app requires email confirmation but does
not send real email. The Registration Confirmation page exposes a local
confirmation link for developer testing. Never use that workflow for public
production registration.

See [page sketches](ui-planning.md) and
[UI style guide](ui-style-guide.md) before building new pages.

## Who Works on What?

Steven:
- Models, services and database
- Authentication and security
- Deployment

David:
- Navigation and layout
- Book list and forms
- Frontend integration

Wadzanai:
- Reading status components
- Filter components
- Responsive design and documentation

## Git Rules

- Create a new branch for your work.
- Do not push directly to main.
- Use pull requests.
- Avoid editing another person's files without discussing it.
- Contact Steven before changing IBookService or Book.
