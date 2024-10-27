using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class Products
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
    }
}
