using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using ReadingTracker.Data;
using ReadingTracker.Models;

namespace ReadingTracker.Services;

public class BookService(
    IDbContextFactory<ApplicationDbContext> dbFactory,
    AuthenticationStateProvider authProvider) : IBookService
{
    private async Task<string> GetUserIdAsync()
    {
        var state = await authProvider.GetAuthenticationStateAsync();
        var userId = state.User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId) ||
            state.User.Identity?.IsAuthenticated != true)
            throw new UnauthorizedAccessException(
                "You must be logged in.");

        return userId;
    }

    private static void Validate(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Title) ||
            book.Title.Trim().Length > 200)
            throw new ArgumentException("Invalid book title.");

        if (string.IsNullOrWhiteSpace(book.Author) ||
            book.Author.Trim().Length > 120)
            throw new ArgumentException("Invalid book author.");

        if (!Enum.IsDefined(book.Status))
            throw new ArgumentException("Invalid reading status.");
    }

    public async Task<List<Book>> GetMyBooksAsync()
    {
        var userId = await GetUserIdAsync();
        await using var db = await dbFactory.CreateDbContextAsync();

        return await db.Books.AsNoTracking()
            .Where(b => b.UserId == userId)
            .OrderBy(b => b.Title)
            .ToListAsync();
    }

    public async Task<Book?> GetMyBookAsync(int id)
    {
        var userId = await GetUserIdAsync();
        await using var db = await dbFactory.CreateDbContextAsync();

        return await db.Books.AsNoTracking()
            .FirstOrDefaultAsync(b =>
                b.Id == id && b.UserId == userId);
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        var userId = await GetUserIdAsync();
        Validate(book);

        await using var db = await dbFactory.CreateDbContextAsync();

        var added = new Book
        {
            Title = book.Title.Trim(),
            Author = book.Author.Trim(),
            Status = book.Status,
            UserId = userId
        };

        db.Books.Add(added);
        await db.SaveChangesAsync();
        return added;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        var userId = await GetUserIdAsync();
        Validate(book);

        await using var db = await dbFactory.CreateDbContextAsync();

        var existing = await db.Books.FirstOrDefaultAsync(b =>
            b.Id == book.Id && b.UserId == userId);

        if (existing == null)
            return false;

        existing.Title = book.Title.Trim();
        existing.Author = book.Author.Trim();
        existing.Status = book.Status;

        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var userId = await GetUserIdAsync();
        await using var db = await dbFactory.CreateDbContextAsync();

        var existing = await db.Books.FirstOrDefaultAsync(b =>
            b.Id == id && b.UserId == userId);

        if (existing == null)
            return false;

        db.Books.Remove(existing);
        await db.SaveChangesAsync();
        return true;
    }
}
