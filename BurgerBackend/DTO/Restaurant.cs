using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.DTO
{
    public class Restaurant
    {
        public string Name { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public int Number { get; set; }
        public String PostCode { get; set; }
        public String GLat { get; set; }
        public String GLong { get; set; }
    }
}
