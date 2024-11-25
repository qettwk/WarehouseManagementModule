using System.Collections.Generic;

namespace Domain
{
    public class Automobile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public ICollection<Category> Categories { get; set; }


    }
}
