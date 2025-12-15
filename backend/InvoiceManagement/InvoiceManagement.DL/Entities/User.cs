using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagement.DL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Pseudo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Id.Equals(user.Id) &&
                   Pseudo == user.Pseudo &&
                   Email == user.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Pseudo, Email);
        }

        public override string ToString()
        {
            return
                $@"Id : {Id} 
                   Pseudo : {Pseudo} 
                   Email : {Email}";
        }

        public static bool operator ==(User a, User b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(User a, User b)
        {
            return !a.Equals(b);
        }
    }
}