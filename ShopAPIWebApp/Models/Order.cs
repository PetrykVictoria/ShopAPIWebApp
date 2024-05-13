using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAPIWebApp.Models
{
    public class Order
    {
            public Order()
            {
                OrderProducts = new List<OrderProduct>();
            }

            public int Id { get; set; }

            [Required(ErrorMessage = "Поле не повинно бути порожнім")]
            [Display(Name = "Статус")]
            public bool Status { get; set; }
            
            public int StoreId { get; set; }
            public virtual Store Store { get; set; }
            public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }

}

