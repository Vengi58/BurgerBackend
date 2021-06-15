﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.DTO
{
    public class Hours
    {
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String MondayOpen { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String MondayClose { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String TuesdayOpen { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String TuesdayClose { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String WednesdayOpen { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String WednesdayClose { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String ThursdayOpen { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String ThursdayClose { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String FridayOpen { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String FridayClose { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String SaturdayOpen { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String SaturdayClose { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String SundayOpen { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public String SundayClose { get; set; }
    }
}