# RT-16 — Reading status and filter components (W05)

Minimal, independent Blazor components for the existing reading status model.
No edits to the database, `BookService`, `Program.cs`, authentication,
or David's Add/Edit/Delete forms are included.

## Components

- `ReadingStatusBadge.razor`: `[Parameter] ReadingStatus Status`.
  Shows a text-and-color Bootstrap badge for Want to Read, Reading and Finished.
- `StatusFilter.razor`: `[Parameter] string Value` plus
  `EventCallback<string> ValueChanged`; supports Blazor `@bind-Value`.
  Values: `All`, `WantToRead`, `Reading`, `Finished`.
  `All` is a UI option, **not** a `ReadingStatus` enum value.

Both components live in `ReadingTracker.Components.Books`.

## Integration in My Books

`ReadingTracker/Components/Pages/MyBooks.razor` imports:

```razor
@using ReadingTracker.Components.Books
```

The previous inline filter was replaced by:

```razor
<StatusFilter @bind-Value="filter" />
```

The previous inline status badge was replaced by:

```razor
<ReadingStatusBadge Status="@book.Status" />
```

The unused `StatusText` and `StatusBadgeClass` methods were removed.
`FilteredBooks`, book CRUD, ownership and navigation remain unchanged.

## Verification

From repository root:

```bash
dotnet build ReadingTracker/ReadingTracker.csproj
```

On the isolated Ubuntu testing worktree, the build passed, and the
tester confirmed registration/login, all four filter options,
Edit/Delete and a smartphone viewport in the browser.
This does not replace a final check on the hosted application.

1. All Books displays all books.
2. Each status filter displays matching books only.
3. Empty filters show the existing empty-filter message.
4. Show all books resets the filter.
5. Badges show readable text for each status.
6. Add/Edit/Delete and login/logout still work.
7. At phone width, controls and book cards do not overflow.

This is a fallback implementation created while Wadzanai's GitHub branch
has no published changes beyond its original baseline. It does not imply
that she has no unpublished local work. Coordinate to avoid duplicated
future changes.
