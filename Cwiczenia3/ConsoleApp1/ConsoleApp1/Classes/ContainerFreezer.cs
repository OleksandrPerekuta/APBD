using ConsoleApp1.Exceptions;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Classes;

public class ContainerFreezer : IContainer
{
    private int _mass;
    private int _height;
    private int _containerMass;
    private int _depth;
    private string _serialNumber;
    private string _type;
    private double _temp;

    private static Dictionary<string, double> map = new()
    {
        { "bananas", 13.3 },
        { "chocolate", 18 },
        { "fish", 2 },
        { "meat", -15 },
        { "ice cream", 18 },
        { "frozen pizza", -30 },
        { "cheese", 7.2 },
        { "sausages", 5 },
        { "butter", 20.5 },
        { "eggs", 19 },
    };
    
    public ContainerFreezer(int mass, int height, int containerMass, int depth, string type, double temperatue)
    {
        _mass = mass;
        _height = height;
        _containerMass = containerMass;
        _depth = depth;
        _serialNumber = "KON-C-"+Guid.NewGuid();
        _type = type;
        _temp = temperatue;
    }
 

    public void FillContainer(int mass, string type)
    {
        if (!_type.Equals(type))
        {
            throw new IncompatibleTypesException("Container containing "+_type+ "cannot be filled with "+type);
        }
        double tmp;
        if (map.TryGetValue(type,out tmp))
        {
            if (tmp>_temp)
            {
                Console.WriteLine("Temperature is changed from default to required for the "+_type);
                _temp = tmp;
            }
            else
            {
                Console.WriteLine("Temperature is not changed while filling");
            }
        }
        else
        {
            Console.WriteLine("No information about best temperature for this product, temperature now is "+_temp+" as of creation");
        }
        FillContainer(mass);
    }

    public void FillContainer(int mass)
    {
        if (_mass+mass> 5000)
        {
            throw new OverfillException("Container cannot be filled with so much " + _type);
        }
        _mass += mass;
    }

    public void EmptyContainer()
    {
        _mass = 0;
    }
    public double GetWeight()
    {
        return _mass + _containerMass;
    }
    public string GetSerialNumber()
    {
        return _serialNumber;
    }

    public void PrintInfo()
    {
        Console.WriteLine("Freezer container {ID: "+_serialNumber+", container mass: "+_containerMass+", mass: "+_mass+", height: "+_height+", depth: "+_depth+", cargo type: "+_type+"}");

    }
}
