namespace API;

public class Animal
{
    public int Id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public double mass { get; set; }
    public string color { get; set; }
}

public class Visit
{
    public DateTime date { get; set; }
    public Animal animal { get; set; }
    public String description { get; set; }
    public double price { get; set; }
}

public interface IMockDB
{
    public ICollection<Animal> GetAllAnimals();
    public Animal? getById(int id);
    public void AddAnimal(Animal animal);
    public void DeleteAnimal(Animal animal);
    public ICollection<Visit> getAllVisits();
    public void AddVisit(Visit visit);
}

public class MockDB : IMockDB
{
    private ICollection<Animal> _animals;
    private ICollection<Visit> _visits;

    public MockDB()
    {
        _animals = new List<Animal>
        {
            new Animal()
            {
                Id = 1,
                name = "Chuj",
                type = "Kot",
                mass = 5,
                color = "Rudy"
            },
            new Animal()
            {
                Id = 2,
                name = "Jamal",
                type = "Pies",
                mass = 25,
                color = "Czarny"
            }
        };

        _visits = new List<Visit>();
    }

    public ICollection<Animal> GetAllAnimals()
    {
        return _animals;
    }

    public Animal? getById(int id)
    {
        return _animals.FirstOrDefault(animal => animal.Id == id);
    }

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }

    public void DeleteAnimal(Animal animal)
    {
        _animals.Remove(animal);
    }

    public ICollection<Visit> getAllVisits()
    {
        return _visits;
    }

    public void AddVisit(Visit visit)
    {
        _visits.Add(visit);
    }
}