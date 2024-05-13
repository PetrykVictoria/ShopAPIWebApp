namespace ShopAPIWebApp.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public bool Weekday { get; set; }
        public string WorkHours { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
