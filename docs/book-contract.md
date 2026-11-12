# Reading Tracker - Book Contract

Status: Draft for team review

## Book Model

Id: int
Title: string (required, max 200 characters)
Author: string (required, max 120 characters)
Status: ReadingStatus
UserId: string

## Reading Status

- WantToRead
- Reading
- Finished

## IBookService

The service will provide these operations:

1. GetMyBooksAsync()
   Return books owned by the logged-in user.

2. GetMyBookAsync(id)
   Return one book belonging to the user.

3. AddBookAsync(...)
   Add a new book.

4. UpdateBookAsync(...)
   Update an existing book.

5. DeleteBookAsync(id)
   Delete a book.

## Security

- Users must be logged in.
- Users can only access their own books.
- The server determines the user ID.
- UserId must never come from an editable form.

## Frontend and Backend

Steven:
Implement Book model, service and database.

Wadzanai:
Build pages using the agreed service methods.

The frontend can use sample data while
the real database service is being developed.

## Next Step

Review this contract together before coding.
