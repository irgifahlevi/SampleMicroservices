using System.ComponentModel.DataAnnotations;

namespace Product.API.Dto
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ProductCode { get; set; }

        public string? Description { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
