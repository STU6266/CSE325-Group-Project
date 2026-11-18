# Reading Tracker - UI Page Plan and Wireframes (RT-03)

**Status:** Detailed draft prepared for David's review. This is **not** a claim
that David approved or implemented the designs.
Pages are implemented by David; Wadzanai owns status/filter components.

**Status:** Reviewed by David on November 18, 2026, with the adjustments listed
in the "David review" section. Wadzanai and Steven review is still pending.
Pages are implemented by David; Wadzanai owns status/filter components.

## Page routes

| Page | Route | Audience | Main action |
| --- | --- | --- | --- |
| Home | `/` | Everyone | Sign in or open My Books |
| My Books | `/books` | Signed-in users | Browse, filter, add |
| Add Book | `/books/add` | Signed-in users | Save a new book |
| Edit Book | `/books/edit/{id:int}` | Owner only | Update title, author, status |
| Delete confirmation | `/books/delete/{id:int}` | Owner only | Confirm deletion |
| Login/Register/Profile | Existing `/Account/*` | Identity pages | Account actions |

All `/books` pages use `[Authorize]`. Signed-out users are redirected to
Login. Existing Identity routes must remain usable.

## Home (desktop and phone)

```text
+--------------------------------------+
| Reading Tracker       Register Login |
+--------------------------------------+
| Track the books you want to read.    |
| Keep your personal reading list.     |
|                                      |
| [My Books]            (signed in)    |
| [Register] [Log in]   (signed out)   |
+--------------------------------------+
```

- Explain what the app does in 1-2 sentences.
- After login, My Books is easy to find.
- Do not display another user's books.
- If signed out, show Login/Register rather than a failing book query.

## My Books (desktop)

```text
+----------------------------------------------------------+
| Reading Tracker       Home  My Books    Profile  Logout  |
+----------------------------------------------------------+
| My Books                                     [+ Add Book] |
| Filter: [All Books v]                                     |
| (success message appears here)                            |
|----------------------------------------------------------|
| +----------------------+     +----------------------+    |
| | Book title           |     | Book title           |    |
| | Author name          |     | Author name          |    |
| | [Reading]            |     | [Finished]           |    |
| | [Edit]  [Delete]     |     | [Edit]  [Delete]     |    |
| +----------------------+     +----------------------+    |
+----------------------------------------------------------+
```

## My Books (phone, about 360px)

```text
+----------------------------+
| [=] Reading Tracker        |
+----------------------------+
| My Books                   |
| [+ Add Book]               |
| Filter: [All Books v]      |
|----------------------------|
| +------------------------+ |
| | Book title             | |
| | Author name            | |
| | [Reading]              | |
| | [Edit]       [Delete]  | |
| +------------------------+ |
| Next book...               |
+----------------------------+
```

- Book cards show Title, Author and Status.
- Edit and Delete operate on the selected book ID. Ownership is checked
  by `IBookService`.
- The filter offers All Books, Want to Read, Reading and Finished.
- Empty collection: "No books yet. Add your first book." with Add Book.
- Empty filter result: "No books match this filter." with Show all books.
- Loading: "Loading your books..."
- Error: "Could not load your books. Please try again." No exception
  details are shown to users.
- Success messages after Add, Edit and Delete appear above the list.

## Add Book (desktop and phone)

```text
+--------------------------------+
| Add Book                       |
| Title *  [_________________]   |
| Author * [_________________]   |
| Status   [Want to Read v]      |
|                                |
| [Save Book]   [Cancel]         |
| Validation messages by field   |
+--------------------------------+
```

- Title and Author are required. Limits must match the Book model
  validation (verify against `docs/book-contract.md`).
- Default status is `WantToRead`.
- `UserId` is never shown or submitted as an editable field.
- Disable Save and show feedback while a save is pending.
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

- Load the book through `IBookService`. Unknown or not-owned IDs show
  "Book not found" and never reveal another user's details.
- Ownership is preserved in the service layer.
- On validation failure, stay on the page and show errors.
- Changing the reading status happens here (PF-06).

## Delete confirmation (separate page)

```text
+--------------------------------+
| Delete Book                    |
| Delete "The Hobbit"?           |
| This cannot be undone.         |
|                                |
| [Cancel]      [Delete Book]    |
+--------------------------------+
```

- Never delete from a single click on My Books. Delete opens this page.
- Cancel returns to My Books and leaves the book unchanged.
- Show success or error feedback and refresh My Books after deletion.
- A missing or other-user book shows "Book not found" and is not deleted.

## Navigation and mobile behavior

Desktop navigation:

| State | Links shown |
| --- | --- |
| Signed out | Home, Register, Login |
| Signed in | Home, My Books, Profile, Logout |

Phone menu (open state):

```text
Signed out              Signed in
+----------------+      +----------------+
| Home           |      | Home           |
| Register       |      | My Books       |
| Login          |      | Profile        |
+----------------+      | Logout         |
                        +----------------+
```

- On mobile, use the existing accessible collapsible navigation pattern.
- Avoid fixed-width cards and forms. No horizontal overflow at about 360px.
- No book route is released without an authorization check.

## Responsibilities

| Work | Owner | Review / dependencies |
| --- | --- | --- |
| Routes, Home, My Books, Add/Edit/Delete UI | David | Steven's backend contract |
| Reading-status badge and filter controls | Wadzanai | David integrates components |
| `IBookService`, validation, authorization, EF Core | Steven | David uses the interface only |
| Visual style and responsive checks | David and Wadzanai | [UI Style Guide](ui-style-guide.md) |

**Do not change David's Razor pages or Wadzanai's components in this documentation PR.**

**Note(From David):** This documentation PR changes documentation only. It does not change Razor pages or components.

## Acceptance cases to check after implementation

| # | Case |
| --- | --- |
| 1 | A signed-out user is redirected to Login on protected book pages. |
| 2 | My Books displays only the current user's books. |
| 3 | Add and Edit validate blanks and length boundaries. |
| 4 | Editing or deleting another user's book is rejected. |
| 5 | Empty library and empty filter results show distinct messages. |
| 6 | Phone layout and keyboard navigation work. |
| 7 | Cancel on the delete page keeps the book unchanged. |

## David review (November 18, 2026)

Adjustments made to the original draft:

| # | Change |
| --- | --- |
| 1 | Routes fixed: `/books`, `/books/add`, `/books/edit/{id:int}`, `/books/delete/{id:int}`. |
| 2 | Delete confirmation is a separate page (`DeleteBook.razor`), not a dialog. |
| 3 | All book pages use `[Authorize]` and redirect signed-out users to Login. |
| 4 | Form limits must match the Book model validation. |
| 5 | Success messages appear on My Books after Add, Edit and Delete. |
| 6 | Open mobile menu sketched for signed-in and signed-out users. |

## Team review required

- [X] David confirms or adjusts routes, wireframes and flow.
- [ ] Wadzanai confirms filter/status component design and accessibility.
- [ ] Steven checks that the UI uses `IBookService` for all operations.
- [ ] Update this document after feedback before closing RT-03.