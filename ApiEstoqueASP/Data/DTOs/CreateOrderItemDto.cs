using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Data.DTOs
{
    public class CreateOrderItemDto
    {

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Especificação da quantidade necessária!")]
        [Range(1, 100, ErrorMessage = "Quantidade deve estar entre 1 e 100 items!")]
        public int Quantity { get; set; }
    }
}
