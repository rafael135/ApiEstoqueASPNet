using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Data.DTOs;

public class CreateProductDto
{
    [Required]
    [MaxLength(80)]
    [MinLength(2)]
    public string Name { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required]
    public double Price { get; set; }
}
