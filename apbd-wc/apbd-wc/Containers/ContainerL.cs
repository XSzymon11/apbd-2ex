using apbd_wc.Interfaces_Exceptions;

namespace apbd_wc.Containers;

public class ContainerL : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public ContainerL(double height, double ownWeight, double depth, double maxCapacity, bool isHazardous) 
        : base(height, ownWeight, depth, maxCapacity)
    {
        this.IsHazardous = isHazardous;
    }

    protected override string GetContainerType() => "L";
    
    public override void LoadCargo(double loadMass)
    {
        double allowedPercentage = IsHazardous ? 0.5 : 0.9;
        double limit = maxCapacity * allowedPercentage;
        
        if (loadMass > limit)
        {
            NotifyHazard($"Przekroczenie dopuszczalnego limitu w kontenerze {serialNumber} (limit = {limit} kg)");
            throw new OverfillException("Masa ładunku przekracza dozwolony limit dla kontenera płynów");
        }
        
        base.LoadCargo(loadMass);
    }
    
    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[HAZARD - L] {message}");
    }
}