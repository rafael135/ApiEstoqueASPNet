using ApiEstoqueASP.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Data.DTOs
{
    public class ReadSupplierDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
