using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Author { get; set; } = string.Empty;

    public ReadingStatus Status { get; set; }
        = ReadingStatus.WantToRead;

    public string UserId { get; set; } = string.Empty;
}
