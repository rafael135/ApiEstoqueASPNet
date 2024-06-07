using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Models
{
    public class OrderItem
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }


        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }


        [Required]
        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }


        public int? OrderId { get; set; }

        [JsonIgnore]
        public virtual Order? Order { get; set; }

    }
}
