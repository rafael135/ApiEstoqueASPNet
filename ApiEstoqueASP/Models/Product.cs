using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Models;

public class Product
{
    [Key]
    [Required]
    public int Id { get; set; }


    [Required]
    [MaxLength(80)]
    [MinLength(2)]
    public string Name { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int InStock { get; set; }

    // Relação 1 - 1 com Supplier
    [Required]
    public int SupplierId { get; set; }

    [JsonIgnore]
    public virtual Supplier Supplier { get; set; }

    [JsonIgnore]
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
