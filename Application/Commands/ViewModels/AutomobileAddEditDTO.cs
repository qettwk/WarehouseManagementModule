using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ViewModels
{
    public class AutomobileAddEditDTO
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public ICollection<Guid> CategoryIDs { get; set; } // Add
    }
}
