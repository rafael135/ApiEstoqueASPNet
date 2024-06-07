using ApiEstoqueASP.Models;
using Newtonsoft.Json;

namespace ApiEstoqueASP.Data.DTOs
{
    public class ReadOrderItemDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }

        public int Quantity { get; set; }
    }
}
