namespace APBD4.Models;

public class Visit
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Animal Animal { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    
    private static List<Visit> Extent { get; set; } = new List<Visit>();
    
    private static int IdCounter { get; set; } = 1;
    
    public Visit(DateTime date, Animal animal, string description, int price)
    {
        if(date == default)
            throw new ArgumentException("Date cannot be default", nameof(date));
        if (animal == null)
            throw new ArgumentNullException(nameof(animal));
        if (string.IsNullOrEmpty(description))
            throw new ArgumentException("Description cannot be null or empty", nameof(description));
        if (price <= 0)
            throw new ArgumentException("Price must be greater than 0", nameof(price));
        Id = IdCounter++;
        Date = date;
        Animal = animal;
        Description = description;
        Price = price;
    }
    
    public static void AddToExtent(Visit visit)
    {
        if (visit == null)
            throw new ArgumentNullException(nameof(visit));
        Extent.Add(visit);
    }
    public static void RemoveFromExtent(Visit visit)
    {
        if (visit == null)
            throw new ArgumentNullException(nameof(visit));
        Extent.Remove(visit);
    }
    public static List<Visit> GetExtent()
    {
        return Extent.AsReadOnly().ToList();
    }

    public static List<Visit> FindByAnimalId(int id)
    {
        return Extent.Where(v => v.Animal.Id == id).ToList();
    }
}