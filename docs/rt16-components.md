# RT-16 Reading Status and Filter Components (W05)

These are minimal reusable Blazor components for the three existing
`ReadingStatus` values. They use Bootstrap and do not access the database
or modify a book. They are fallback implementations if Wadzanai's code is
not yet available.

## Files

- `ReadingTracker/Components/Books/ReadingStatusBadge.razor`:
  `<ReadingStatusBadge Status="@book.Status" />`. Shows a readable
  "Want to Read", "Reading", or "Finished" badge.
- `ReadingTracker/Components/Books/StatusFilter.razor`:
  `<StatusFilter @bind-Value="filter" />`, where the parent has
  `private string filter = "All";`. A `ValueChanged` callback tells
  the parent when the dropdown changes. Options: All Books, Want to Read,
  Reading and Finished. "All" is only a UI option, not an enum value.

`MyBooks.razor` demonstrates both components and keeps its existing
`FilteredBooks` logic unchanged. Filtering happens **only in the UI**;
`IBookService`, the database, authentication and Add/Edit/Delete forms
are untouched.

## Build and manual W05 test

From the repository root:

```bash
dotnet build ReadingTracker/ReadingTracker.csproj
```

Run the Blazor app, log in and create three books, one with each status.
On `/books` check:

1. All Books shows all three books.
2. Each filter shows only books with that status.
3. A filter with no matches shows the existing empty-filter message.
4. "Show all books" restores all books.
5. Each card displays its correct text badge.
6. Edit/Delete still work; refresh and log back in to check persistence.
7. Repeat the filter test in a narrow mobile viewport.

**Team workflow:** Review this PR before merging. If Wadzanai later
publishes her own components, compare her API and replace these only
after David approves the integration. Do not combine with unrelated
page, service or database changes.
