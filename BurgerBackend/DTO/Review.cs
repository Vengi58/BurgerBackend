using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.DTO
{
    public class Review
    {
        [Required]
        [Range(1, 5)]
        public int Taste { get; set; }
        [Required]
        [Range(1, 5)]
        public int Texture { get; set; }
        [Required]
        [Range(1, 5)]
        public int VisualRepresentation { get; set; }
    }
}
