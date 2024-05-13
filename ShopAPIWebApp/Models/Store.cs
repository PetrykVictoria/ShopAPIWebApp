using System.ComponentModel.DataAnnotations;
namespace ShopAPIWebApp.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    }
}
