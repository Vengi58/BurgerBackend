using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public int Number { get; set; }
        public String PostCode { get; set; }
        public String GLat { get; set; }
        public String GLong { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}
