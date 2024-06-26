﻿using System.ComponentModel.DataAnnotations;

namespace ShopAPIWebApp.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
