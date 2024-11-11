using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RecordWrite
    {
        public Guid Id { get; set; }
        public Guid AutomobileID { get; set; }
        public int Value { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
