# Reading Tracker - UI Page Plan and Wireframes (RT-03)

Status: Detailed draft prepared for David's review. This is **not** a claim
that David approved or implemented the designs.
Pages are implemented by David; Wadzanai owns status/filter components.

## Page routes (proposed)

| Page | Route suggestion | Audience | Main action |
| --- | --- | --- | --- |
| Home | `/` | Everyone | Sign in or open My Books |
| My Books | `/books` | Signed-in users | Browse, filter, add |
| Add Book | `/books/add` | Signed-in users | Save a new book |
| Edit Book | `/books/edit/{id:int}` | Owner only | Update title/author/status |
| Delete confirmation | Inline/dialog on My Books or Edit | Owner only | Confirm deletion |
| Login/Register/Profile | Existing `/Account/*` | Identity pages | Account actions |

Routes for the new book pages are **suggestions**; David can choose
different route names. Existing Identity routes must remain usable.

## Home (desktop and phone)

```text
+--------------------------------------+
| Reading Tracker              Login   |
+--------------------------------------+
| Track the books you want to read.    |
| Keep your personal reading list.    |
|                                      |
| [My Books] (signed in)               |
| [Register] [Log in] (signed out)     |
+--------------------------------------+
```

- Explain what the app does in 1-2 sentences.
- After login, My Books is easy to find.
- Do not display another user's books.
- If signed out, show Login/Register rather than a failing book query.

## My Books - desktop

```text
+----------------------------------------------------------+
| Reading Tracker            My Books     Profile / Logout |
+----------------------------------------------------------+
| My Books                                     [+ Add Book] |
| Filter: [All Books v]                                     |
|----------------------------------------------------------|
| [Book title]                  [Book title]               |
| Author name                   Author name                |
| [Reading]                     [Finished]                 |
| [Edit] [Delete]               [Edit] [Delete]            |
+----------------------------------------------------------+
```

## My Books - narrow phone

```text
+----------------------------+
| Menu    Reading Tracker    |
+----------------------------+
| My Books                   |
| [+ Add Book]               |
| Filter: [All Books v]      |
|----------------------------|
| Book title                 |
| Author name                |
| [Reading]                  |
| [Edit]       [Delete]      |
|----------------------------|
| Next book...               |
+----------------------------+
```

- Book cards show **Title, Author, Status**.
- Edit/Delete operate on the selected book ID and check ownership via
  `IBookService`.
- The filter includes All Books, Want to Read, Reading and Finished.
- Empty collection: "No books yet. Add your first book." with **Add Book**.
- Empty filter result: "No books match this filter." with **Show all books**.
- Loading state: short "Loading your books..." message.
- Error: "Could not load your books. Please try again." No internal exception
  details displayed to users.

## Add Book (same form on phone and desktop)

```text
+--------------------------------+
| Add Book                       |
| Title *  [_________________]   |
| Author * [_________________]   |
| Status   [Want to Read v]      |
|                                |
| [Save Book]   [Cancel]         |
| Validation message if needed   |
+--------------------------------+
```

- Title is required, maximum 200 characters.
- Author is required, maximum 120 characters.
- Default status is `WantToRead`.
- `UserId` is never shown in the UI or submitted as an editable field.
- Disable Save and display feedback while a save is pending.
- After success, return to My Books with a confirmation.

## Edit Book

```text
+--------------------------------+
| Edit Book                      |
| Title *  [existing title___]   |
| Author * [existing author__]   |
| Status   [Reading v]           |
|                                |
| [Save Changes] [Cancel]        |
+--------------------------------+
```

- Load by `GetMyBookAsync(id)`; unknown / not-owned IDs show "Book not found"
  rather than the other user's details.
- Preserve the book ID and current user ownership in the service layer.
- Validate inputs; on failure remain on the page and show errors.

## Delete confirmation

```text
Delete "The Hobbit"?
This cannot be undone.
[Cancel]   [Delete Book]
```

- Never delete immediately from a single accidental click.
- Show clear success/error feedback. Refresh My Books after deletion.
- A missing or other-user book must not be deleted.

## Navigation and mobile behavior

- Signed-out: Home, Register, Login.
- Signed-in: Home, My Books, Profile, Logout.
- On mobile, use the existing accessible collapsible navigation pattern.
- Avoid fixed-width cards/forms and horizontal overflow at ~360px width.
- No new Book routes should be released without an authorization/identity check.

## Responsibilities and acceptance tests

| Work | Owner | Review / dependencies |
| --- | --- | --- |
| Page routes, Home, My Books, Add/Edit and delete UI | David | Steven backend contract |
| Reading-status badge and filter controls | Wadzanai | David integrates components |
| `IBookService`, validation, authorization, EF Core | Steven | David uses interface only |
| Visual style and responsive checks | David + Wadzanai | [UI Style Guide](ui-style-guide.md) |

**Do not change David's Razor pages or Wadzanai's components in this documentation PR.**

Acceptance cases to check after implementation:
1. Unauthenticated user gets a clear login prompt on protected book pages.
2. My Books displays only the current user's books.
3. Add/Edit validate blanks and length boundaries.
4. Editing/deleting another user's book is rejected.
5. Empty library and filter results display distinct messages.
6. Phone layout and keyboard navigation work.

## Team review required

- [ ] David confirms or adjusts routes, wireframes and flow.
- [ ] Wadzanai confirms filter/status component design and accessibility.
- [ ] Steven checks that the UI uses `IBookService` for all operations.
- [ ] Update this document after feedback before closing RT-03.
