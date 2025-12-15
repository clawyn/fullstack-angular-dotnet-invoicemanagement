using InvoiceManagement.DL.Entities;
using InvoiceManagement.ToolBox.Repositories;

namespace InvoiceManagement.BLL.Services.Interfaces
{
    public interface IUserService
    {
        void Register(User user);
        User Login(string login, string password);
    }
}