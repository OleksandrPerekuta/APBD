namespace WebApplication1.Classes;
using WebApplication1.Interfaces;

public class AnimalDb : IAnimalDb
{
    private readonly ICollection<Animal> _animals;

    public AnimalDb()
    {
        _animals = new List<Animal>();
        _animals.Add(new Animal
        {
            Name = "Chmel",
            Kategoria = "Monkey",
            Weight = 33,
            WoolColor = "Red"
        });
        _animals.Add(new Animal
        {
            Name = "Bartek",
            Kategoria = "Cat",
            Weight = 12,
            WoolColor = "Black"
        });
        _animals.Add(new Animal
        {
            Name = "Kusik",
            Kategoria = "Dog",
            Weight = 20,
            WoolColor = "White"
        });
    }
    public ICollection<Animal> GetAll()
    {
        return _animals;
    }

    public Animal? GetById(int id)
    {
        return _animals.FirstOrDefault(animal => animal.Id == id );
    }

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }

    public bool UpdateAnimal(Animal animal, int id)
    {
        var animalToRemove = GetById(id);
        if(animalToRemove is null) return false;
        _animals.Remove(animal);
        animal.SetId(id);
        AddAnimal(animal);
        return true;
    }

    public bool RemoveAnimalById(int id)
    {
        var toRemove = _animals.FirstOrDefault(animal => animal.Id == id );
        if (toRemove != null) return _animals.Remove(toRemove);
        return false;
    }
}