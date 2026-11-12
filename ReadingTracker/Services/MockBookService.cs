using ReadingTracker.Models;

namespace ReadingTracker.Services;

public class MockBookService : IBookService
{
    private readonly List<Book> _books = new()
    {
        new Book { Id = 1, Title = "The Hobbit",
            Author = "J. R. R. Tolkien",
            Status = ReadingStatus.Finished, UserId = "demo" },

        new Book { Id = 2, Title = "Dune",
            Author = "Frank Herbert",
            Status = ReadingStatus.Reading, UserId = "demo" },

        new Book { Id = 3, Title = "The Hunger Games",
            Author = "Suzanne Collins",
            Status = ReadingStatus.WantToRead, UserId = "demo" }
    };

    private int _nextId = 4;

    private static Book Copy(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        Status = book.Status,
        UserId = book.UserId
    };

    public Task<List<Book>> GetMyBooksAsync()
    {
        return Task.FromResult(_books.Select(Copy).ToList());
    }

    public Task<Book?> GetMyBookAsync(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book == null ? null : Copy(book));
    }

    public Task<Book> AddBookAsync(Book book)
    {
        var added = Copy(book);
        added.Id = _nextId++;
        added.UserId = "demo";
        _books.Add(added);
        return Task.FromResult(Copy(added));
    }

    public Task<bool> UpdateBookAsync(Book book)
    {
        var existing = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existing == null)
            return Task.FromResult(false);

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Status = book.Status;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteBookAsync(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return Task.FromResult(false);

        _books.Remove(book);
        return Task.FromResult(true);
    }
}
