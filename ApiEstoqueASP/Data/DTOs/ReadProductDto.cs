using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Data.DTOs;

public class ReadProductDto
{
    public int Id { get; set; }


    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }
}
