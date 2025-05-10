namespace APBD4.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public float Weight { get; set; }
    public string FurColor { get; set; }
    private static List<Animal> Extent { get; set; } = new List<Animal>();
    
    private static int IdCounter { get; set; } = 1;
    
    public Animal(string name, string category, float weight, string furColor)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(category))
            throw new ArgumentException("Category cannot be null or empty", nameof(category));
        if (weight <= 0)
            throw new ArgumentException("Weight must be greater than 0", nameof(weight));
        if (string.IsNullOrEmpty(furColor))
            throw new ArgumentException("FurColor cannot be null or empty", nameof(furColor));

        Id = IdCounter++;
        Name = name;
        Category = category;
        Weight = weight;
        FurColor = furColor;

        // Didn't added animal to Extent in constructor because of the edit API operation
    }
    
    public static void AddToExtent(Animal animal)
    {
        if (animal == null)
            throw new ArgumentNullException(nameof(animal));
        Extent.Add(animal);
    }
    public static void RemoveFromExtent(Animal animal)
    {
        if (animal == null)
            throw new ArgumentNullException(nameof(animal));
        Extent.Remove(animal);
    }
    public static List<Animal> GetExtent()
    {
        return Extent.AsReadOnly().ToList();
    }
    public static Animal FindAnimalById(int id)
    {
        return Extent.FirstOrDefault(a => a.Id == id);
    }
    
    public void EditAnimal(Animal animal)
    {
        if (animal == null)
            throw new ArgumentNullException(nameof(animal));
        Name = animal.Name;
        Category = animal.Category;
        Weight = animal.Weight;
        FurColor = animal.FurColor;
    }
}