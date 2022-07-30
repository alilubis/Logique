using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logique.Models.Entities
{
    public class User
    {
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be atleast {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password required")]
        public string ConfirmPassword { get; set; }
    }
}
