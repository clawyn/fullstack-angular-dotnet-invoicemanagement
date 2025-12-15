namespace InvoiceManagement.DL.Entities;


public class Product
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public String? Category { get; set; }
    
    public Decimal Price { get; set; }
    
    public String? Specification  { get; set; }
    
    public ICollection<InvoiceProduct> InvoiceProducts { get; set; }
    
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (ReferenceEquals(this, obj))
            return true;
        Product? other = obj as Product;
        if (other == null) return false;
        return Id == other.Id && ProductName == other.ProductName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, ProductName);
    }

    public override string ToString()
    {
        return 
            $@"Id : {Id} 
                   Name : {ProductName}";
    }

    public static bool operator ==(Product a, Product b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Product a, Product b)
    {
        return !a.Equals(b);
    }
}
