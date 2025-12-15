namespace InvoiceManagement.DL.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public String Address { get; set; }
    public String Email { get; set; }
    public String Phone { get; set; }
    
    public List<Invoice> Invoices { get; set; }
    
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

    public static bool operator ==(Customer a, Customer b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Customer a, Customer b)
    {
        return !a.Equals(b);
    }
}