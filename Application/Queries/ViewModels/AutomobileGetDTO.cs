using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ViewModels
{
    public class AutomobileGetDTO
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public ICollection<string> CategoriesName { get; set; } // Get
    }
}
