using InvoiceManagement.API.Dtos.User;
using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.API.Mappeurs
{
    public static class UserMappeur
    {
        public static User ToUser(this RegisterFormDto form)
        {
            return new User
            {
                Pseudo = form.Pseudo,
                Email = form.Email,
                Password = form.password,
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Pseudo = user.Pseudo,
            };
        }
    }
}