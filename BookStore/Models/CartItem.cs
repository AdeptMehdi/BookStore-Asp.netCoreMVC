namespace BookStore.Models
{
public class CartItem
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; } // تغییر از double به decimal
    public int Quantity { get; set; }
}


}
