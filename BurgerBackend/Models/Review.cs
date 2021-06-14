using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [Range(1, 5)]
        public int Taste { get; set; }
        [Range(1, 5)]
        public int Texture { get; set; }
        [Range(1, 5)]
        public int VisualRepresentation { get; set; }

        public int RestaurantID { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
