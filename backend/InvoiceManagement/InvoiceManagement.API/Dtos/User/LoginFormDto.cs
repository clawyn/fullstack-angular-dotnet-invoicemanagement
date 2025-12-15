using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.API.Dtos.User
{
    public class LoginFormDto
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}    

