using System.Security.Claims;

namespace InvoiceManagement.API.Extensions
{
    public static class ExtendedUser
    {

        public static Guid GetId(this ClaimsPrincipal principal)
        {
            string? id = principal.FindFirst(ClaimTypes.Sid)?.Value;
            return id is not null ? new Guid(id) : Guid.Empty ;
        }
    }
}    
