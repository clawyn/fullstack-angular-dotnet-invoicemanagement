using InvoiceManagement.BLL.Services.Interfaces;
using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.DL.Entities;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagement.BLL.Services
{
    public class UserService : IUserService
    {

        private readonly IUserManagementRepository _userManagementRepository;

        public UserService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public User Login(string login, string password)
        {
            User? user = _userManagementRepository.FindOne(u => u.Pseudo == login || u.Email == login);
            if(user is null)
            {
                throw new Exception("User doesn't exist");
            }
            if (!Argon2.Verify(user.Password,password))
            {
                throw new Exception("Wrong password");
            }
            return user;
        }

        public void Register(User user)
        {
            if(_userManagementRepository
               .Any(u => u.Pseudo == user.Pseudo || u.Email == user.Email))
            {
                throw new Exception($"User already exist");
            }

            user.Password = Argon2.Hash(user.Password);
            _userManagementRepository.Save(user);
        }
    }
}