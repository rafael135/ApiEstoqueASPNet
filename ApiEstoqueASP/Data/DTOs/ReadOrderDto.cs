using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Data.DTOs
{
    public class ReadOrderDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public double TotalPrice { get; set; }

        public DateTime Performed { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
