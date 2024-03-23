namespace ConsoleApp1.Interfaces;

public interface IContainer
{
    void FillContainer(int mass);
    void EmptyContainer();
    double GetWeight();
    string GetSerialNumber();
    void PrintInfo();
}