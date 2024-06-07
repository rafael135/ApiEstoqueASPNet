using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Data.DTOs
{
    public class CreateOrderDto
    {
        public double TotalPrice { get; set; } = 0;

        [Required]
        public string UserId { get; set; }

        public DateTime Performed { get; set; } = DateTime.Now;
    }
}
