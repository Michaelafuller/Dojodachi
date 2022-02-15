using System.ComponentModel.DataAnnotations;

namespace Dojodachi.Models
{
    public class Pet
    {
        [Required]
        public int Fullness { get; set; }

        public int Happiness { get; set; }

        [Required(ErrorMessage = "You gotta work for them meals, first!")]
        [Range(0,100)]
        public int Meals { get; set; }

        // [Required(ErrorMessage = "Your pet doesn't have the energy to be playing right now!")]
        // [Range(0,200)]
        public int Energy { get; set; }
    }
}