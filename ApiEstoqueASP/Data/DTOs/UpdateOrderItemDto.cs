using ApiEstoqueASP.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Data.DTOs
{
    public class UpdateOrderItemDto
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int? OrderId { get; set; }
    }
}
