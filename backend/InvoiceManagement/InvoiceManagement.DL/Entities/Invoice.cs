using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagement.DL.Entities;

public class Invoice
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public decimal WorkForce { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public Customer Customer { get; set; }
    
    public ICollection<InvoiceProduct> InvoiceProducts { get; set; }

    
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    
    
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (ReferenceEquals(this, obj))
            return true;
        Invoice? other = obj as Invoice;
        if (other == null) return false;
        return Id == other.Id && Name == other.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

    public override string ToString()
    {
        return 
            $@"Id : {Id} 
                   Name : {Name}";
    }

    public static bool operator ==(Invoice a, Invoice b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Invoice a, Invoice b)
    {
        return !a.Equals(b);
    }
}
