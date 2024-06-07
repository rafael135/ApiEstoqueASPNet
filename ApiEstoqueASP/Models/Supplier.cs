using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Models
{
    public class Supplier
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(length: 90, ErrorMessage = "Número máximo de caracteres atingido!")]
        [MinLength(length: 2, ErrorMessage = "Deve ter no mínimo 2 caracteres!")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; }

        // Relação 1 - N com Produtos
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
