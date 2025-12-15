using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.API.Dtos.Customer
{
    public class CustomerFormDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        
        [Required]
        [MaxLength(150)]
        public String Address { get; set; } = null!;
        
        [Required]
        [MaxLength(150)]
        public String Email { get; set; } = null!;
        
        [Required]
        [MaxLength(20)]
        public String Phone { get; set; } = null!;
    }
}


