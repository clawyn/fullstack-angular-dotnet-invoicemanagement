namespace InvoiceManagement.API.Dtos.User
{
    public class UserTokenDto
    {
        public string Token { get; set; } = null!;
        public UserDto User { get; set; } = null!;
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string Pseudo { get; set; } = null!;
    } 
}    