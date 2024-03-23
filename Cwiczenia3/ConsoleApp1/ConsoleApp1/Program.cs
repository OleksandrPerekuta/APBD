// See https://aka.ms/new-console-template for more information
using ConsoleApp1.Classes;
using ConsoleApp1.Interfaces;

Ship ship = new Ship(10, 8, 1000);
ContainerLiquid containerLiquid1 = new(100, 4, 1000, 50);
ContainerGas containerGas1 = new(100, 4, 400, 50);
ContainerFreezer containerFreezer1 = new ContainerFreezer(10, 4, 200, 50, "banana", -12);
        
ContainerLiquid containerLiquid2 = new(100, 4, 1000, 50);
ContainerGas containerGas2 = new(100, 4, 400, 50);
ContainerFreezer containerFreezer2 = new ContainerFreezer(1000, 4, 200, 50, "banana", -12);

ContainerLiquid containerLiquid3 = new(100, 4, 1000, 50);
ContainerGas containerGas3 = new(100, 4, 400, 50);
ContainerFreezer containerFreezer3 = new ContainerFreezer(100, 4, 200, 50, "banana", -12);

ContainerLiquid containerLiquid4 = new(100, 4, 1000, 50);
ContainerGas containerGas4 = new(100, 4, 400, 50);
ContainerFreezer containerFreezer4 = new ContainerFreezer(100, 4, 200, 50, "banana", -12);

ship.AddContainer(containerLiquid1);

List<IContainer> lst = new List<IContainer>()
{
    containerFreezer1, containerFreezer2, containerFreezer3, containerFreezer4,
    containerLiquid1, containerLiquid2, containerLiquid3, containerLiquid4,
    containerGas1, containerGas2, containerGas3, containerGas4
};
ship.AddListOfContainers(lst);
Ship ship2 = new Ship(11, 4, 3000);
Console.WriteLine(Ship.MoveContainer(ship, ship2, containerLiquid1));
ship.PrintInfo();
ship2.PrintInfo();
containerFreezer2.PrintInfo();
