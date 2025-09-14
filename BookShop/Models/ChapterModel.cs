using BookStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Chapters")]
public class ChapterModel
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public BookModel Book { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}