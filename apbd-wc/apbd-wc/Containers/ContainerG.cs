using apbd_wc.Interfaces_Exceptions;

namespace apbd_wc.Containers;

public class ContainerG : Container, IHazardNotifier 
{
    public double pressure { get; private set; }

    public ContainerG(double height, double ownWeight, double depth, double maxCapacity, double pressure)
        : base(height, ownWeight, depth, maxCapacity)
    {
        this.pressure = pressure;
    }

    protected override string GetContainerType() => "G";
    
    public override void EmptyCargo()
    {
        cargoMass *= 0.05;
    }

    public override void LoadCargo(double loadMass)
    {
        if (loadMass > maxCapacity)
        {
            NotifyHazard($"Przekroczenie dopuszczalnej ładowności w kontenerze {serialNumber}");
            throw new OverfillException("Masa ładunku przekracza dozwolony limit dla kontenera na gaz");
        }
        base.LoadCargo(loadMass);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[HAZARD - G] {message}");
    }
}