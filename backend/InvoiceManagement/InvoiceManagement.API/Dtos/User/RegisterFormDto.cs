using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.API.Dtos.User
{
    public class RegisterFormDto
    {
        [Required]
        [MaxLength(50)]
        public string Pseudo { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string password { get; set; } = null!;
    }
}    
