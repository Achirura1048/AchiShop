using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.Models
{
    public class Country
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string ISO2 { get; set; }

        
        public ICollection<State> States { get; set; }
    }
}
