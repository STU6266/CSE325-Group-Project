using ReadingTracker.Models;

namespace ReadingTracker.Services;

public interface IBookService
{
    Task<List<Book>> GetMyBooksAsync();

    Task<Book?> GetMyBookAsync(int id);

    Task<Book> AddBookAsync(Book book);

    Task<bool> UpdateBookAsync(Book book);

    Task<bool> DeleteBookAsync(int id);
}
