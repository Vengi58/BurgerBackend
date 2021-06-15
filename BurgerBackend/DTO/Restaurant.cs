using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.DTO
{
    public class Restaurant
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
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
        [Range(1,10000)]
        public int Number { get; set; }
        [Required]
        [Range(1, 10000)]
        public String PostCode { get; set; }
        [Required]
        public Hours Hours { get; set; }
    }
}
