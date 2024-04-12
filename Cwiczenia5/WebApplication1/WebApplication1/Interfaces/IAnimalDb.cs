namespace WebApplication1.Interfaces;
using WebApplication1.Classes;

public interface IAnimalDb
{
    public ICollection<Animal> GetAll();
    public Animal? GetById(int id);
    public void AddAnimal(Animal animal);
    public bool UpdateAnimal(Animal animal, int id);
    public bool RemoveAnimalById(int id);
    

}