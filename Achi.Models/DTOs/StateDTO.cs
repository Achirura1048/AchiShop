using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.Models.DTOs
{
    public class StateDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string state_code { get; set; }
        public string country_code { get; set; }

        public string iso2 { get; set; }
    }
}
