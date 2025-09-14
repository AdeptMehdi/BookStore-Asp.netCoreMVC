using BookStore.Models;

public class ChapterModel
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public BookModel Book { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}