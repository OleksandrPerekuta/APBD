using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Classes;

public class Ship
{
    private int _speed;
    private int _maxNumberOfContainers;
    private int _maxWeight;
    private List<IContainer> _containers = new List<IContainer>();

    public Ship(int speed, int maxNumberOfContainers, int maxWeight)
    {
        _speed = speed;
        _maxNumberOfContainers = maxNumberOfContainers;
        _maxWeight = maxWeight;
    }

    public bool AddContainer(IContainer container)
    {
        bool isAdded;
        if (_containers.Count + 1 < _maxNumberOfContainers &&
            container.GetWeight() + GetTotalWeight(_containers) < _maxWeight)
        {
            _containers.Add(container);
            isAdded = true;
        }
        else
        {
            Console.WriteLine("Ship is overfilled");
            isAdded = false;
        }

        return isAdded;
    }

    public void AddListOfContainers(List<IContainer> list)
    {
        if (_containers.Count + list.Count < _maxNumberOfContainers &&
            GetTotalWeight(_containers) + GetTotalWeight(list) < _maxWeight)
        {
            list.AddRange(list);
        }
        else
        {
            Console.WriteLine("Ship is overfilled");
        }
    }

    public bool RemoveContainer(IContainer container)
    {
        bool isRemoved = _containers.Remove(container);
        if (isRemoved)
        {
            _maxWeight -= (int)container.GetWeight();
        }

        return isRemoved;
    }

    private bool RemoveContainer(string serialNumber)
    {
        bool isRemoved = false;
        foreach (IContainer container in _containers)
        {
            if (container.GetSerialNumber().Equals(serialNumber))
            {
                RemoveContainer(container);
                isRemoved = true;
            }
        }

        return isRemoved;
    }

    public void ReplaceContainer(string serialOfToBeRemoved, IContainer toBePlaced)
    {
        RemoveContainer(serialOfToBeRemoved);
        AddContainer(toBePlaced);
    }

    private double GetTotalWeight(List<IContainer> containers)
    {
        double totalWeight = 0;
        foreach (var container in containers)
        {
            totalWeight += container.GetWeight();
        }

        return totalWeight;
    }

    public static bool MoveContainer(Ship sourceShip, Ship destinationShip, IContainer container)
    {
        if (sourceShip.RemoveContainer(container))
        {
            if (destinationShip.AddContainer(container))
            {
                return true;
            }

            sourceShip.AddContainer(container);
        }

        return false;
    }

    public void PrintInfo()
    {
        Console.WriteLine("Transport ship {max speed: "+_speed+", max weight: "+_maxWeight+", max number of containers: "+_maxNumberOfContainers+"}");
    }
}
