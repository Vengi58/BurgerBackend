using System.ComponentModel.DataAnnotations;

namespace BurgerBackend.Models
{
    public class Review
    {
        [Key]
        [Required]
        public int ReviewID { get; set; }
        [Required]
        public int Taste { get; set; }
        [Required]
        public int Texture { get; set; }
        [Required]
        public int VisualRepresentation { get; set; }

        [Required]
        public string RestaurantName { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
