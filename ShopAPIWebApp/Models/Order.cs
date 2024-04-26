using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAPIWebApp.Models
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Статус")]
        public bool Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
