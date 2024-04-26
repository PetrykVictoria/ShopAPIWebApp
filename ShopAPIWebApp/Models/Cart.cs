using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShopAPIWebApp.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public int Id { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }

}
