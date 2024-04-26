using System.ComponentModel.DataAnnotations;

namespace ShopAPIWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ціна")]
        public int Price { get; set; }

        [Display(Name = "Кількість")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Наявність")]
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
