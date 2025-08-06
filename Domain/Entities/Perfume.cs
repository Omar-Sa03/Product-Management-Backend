using Domain.Common;

namespace Domain.Entities
{
    public class Perfume : Entity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Size { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
    }
}
