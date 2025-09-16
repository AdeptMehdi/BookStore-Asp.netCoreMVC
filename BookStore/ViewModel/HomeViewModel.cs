namespace BookStore.Models
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; } = new();
        public List<Book> NewBooks { get; set; } = new();
        public List<Book> BestSellers { get; set; } = new();
        public List<Book> FeaturedBooks { get; set; } = new();
    }
}