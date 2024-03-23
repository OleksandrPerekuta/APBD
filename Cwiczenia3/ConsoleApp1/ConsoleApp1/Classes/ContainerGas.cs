using ConsoleApp1.Exceptions;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Classes;

public class ContainerGas : IContainer, IHazardNotifier
{
    private double _mass;
    private int _height;
    private int _containerMass;
    private int _depth;
    private string _serialNumber;
    private int _pressure;
    private int _volume;


    public ContainerGas(int mass, int height, int containerMass, int depth)
    {
        _mass = mass;
        _height = height;
        _containerMass = containerMass;
        _depth = depth;
        _serialNumber = "KON-G-"+Guid.NewGuid();
        _volume = height * height * depth;

    }

    public void FillContainer(int mass)
    {
        HazardContentsNotifier();
        if (_mass+mass >_volume)
        {
            throw new OverfillException("Container cannot fit so much gas");
        }
        _mass += mass;
    }

    public void EmptyContainer()
    {
        _mass = 0.05 * _mass;
    }

    public void HazardContentsNotifier()
    {
        Console.WriteLine("Container: " + _serialNumber + " is being filled with dangerous substances");
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
        Console.WriteLine("Gas container {ID: "+_serialNumber+", container mass: "+_containerMass+", mass: "+_mass+", height: "+_height+", depth: "+_depth+", pressure: "+_pressure+"}");

    }
}
