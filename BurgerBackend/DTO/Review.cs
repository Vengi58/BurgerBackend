using System;
using System.ComponentModel.DataAnnotations;

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
