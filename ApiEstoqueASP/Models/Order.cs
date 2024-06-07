using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Models
{
    public class Order
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        public double TotalPrice { get; set; }

        [Required]
        public DateTime Performed { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
