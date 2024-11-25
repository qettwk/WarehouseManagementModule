using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ViewModels
{
    public class CheckedAutoIDsDTO
    {
        public bool excepted { get; set; }
        public List<Guid> AutomobileIDs { get; set; } = new List<Guid>();
        public List<int> Counts { get; set; } = new List<int>();
        public decimal TotalSum { get; set; }
    }
}
