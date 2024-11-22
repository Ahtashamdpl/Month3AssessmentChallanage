using System.ComponentModel.DataAnnotations;

namespace Month3AssessmentCode.Models
{
    public class RegisterModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100,MinimumLength =6)]
        public string Password { get; set; }
    }
}
