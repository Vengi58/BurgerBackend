using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BurgerBackend.Models
{
    public class Restaurant
    {
        [Key]
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public virtual Hours Hours { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public String Country { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public String City { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public String Street { get; set; }
        [Required]
        [Range(1, 10000)]
        public int Number { get; set; }
        [Required]
        [Range(1, 10000)]
        public String PostCode { get; set; }
        public Double GLat { get; set; }
        public Double GLong { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}
