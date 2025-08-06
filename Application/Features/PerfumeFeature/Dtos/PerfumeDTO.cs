using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PerfumeFeature.Dtos
{
    public class PerfumeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Size { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
    }
}
