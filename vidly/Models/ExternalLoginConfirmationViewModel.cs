using System.ComponentModel.DataAnnotations;

namespace vidly.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Drivers License")]
        public string DriversLicense { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
    }
}