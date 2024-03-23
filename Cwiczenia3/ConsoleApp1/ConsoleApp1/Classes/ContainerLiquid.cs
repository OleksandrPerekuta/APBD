using ConsoleApp1.Exceptions;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Classes;

public class ContainerLiquid : IContainer, IHazardNotifier
{
    private double _mass;
    private int _height;
    private int _containerMass;
    private int _depth;
    private string _serialNumber;
    private int _volume;
    private bool _isDangerous;
    private string _type;

    public ContainerLiquid(int mass, int height, int containerMass, int depth)
    {
        _height = height;
        _containerMass = containerMass;
        _depth = depth;
        _serialNumber = "KON-L-" + Guid.NewGuid();
        _volume = height * height * depth;
        FillContainer(mass);
    }

    public void FillContainer(int mass, string type, bool isDangerous)
    {
        if (!_type.Equals(type))
        {
            throw new IncompatibleTypesException("Container containing "+_type+ "cannot be filled with "+type);
        }

        _isDangerous = isDangerous;
        if (isDangerous)
        {
            HazardContentsNotifier();
        }

        FillContainer(mass);
    }

    public void FillContainer(int mass)
    {
        double limit = _isDangerous ? 0.5 * _volume : 0.9 * _volume;
        if (_mass + mass > limit)
        {
            throw new OverfillException("More substances can not be added, filling interrupted");
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
        Console.WriteLine("Liquid Container {ID: "+_serialNumber+", container mass: "+_containerMass+", mass: "+_mass+", height: "+_height+", depth: "+_depth+", cargo type: "+_type+"}");
    }

    
    public void HazardContentsNotifier()
    {
        if (_isDangerous)
        {
            Console.WriteLine("Container: " + _serialNumber + " is being filled with dangerous substances");
        }
    }
}
