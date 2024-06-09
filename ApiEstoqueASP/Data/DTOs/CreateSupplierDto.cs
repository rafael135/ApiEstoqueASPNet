using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Data.DTOs;

public class CreateSupplierDto
{
    [Required]
    [MaxLength(length: 90, ErrorMessage = "Número máximo de caracteres atingido!")]
    [MinLength(length: 2, ErrorMessage = "Deve ter no mínimo 2 caracteres!")]
    public string? Name { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime RegisterDate { get; set; } = DateTime.Now;
}
