using apbd_wc.Interfaces_Exceptions;

namespace apbd_wc.Containers;

public abstract class Container
{
    private static int idCounter = 0;
    public virtual double cargoMass { get; protected set; }
    public virtual double height { get; protected set; }
    public virtual double ownWeight { get; protected set; }
    public virtual double depth { get; protected set; }
    public virtual string serialNumber { get; protected set; }
    public virtual double maxCapacity { get; protected set; }

    protected Container(double height, double ownWeight, double depth, double maxCapacity)
    {
        this.height = height;
        this.ownWeight = ownWeight;
        this.depth = depth;
        this.maxCapacity = maxCapacity;
        ++idCounter; //unikalny numer
        serialNumber = $"KON-{GetContainerType()}-{idCounter:D5}"; // Format numeru: KON-{typ}-{kolejnyNumer} D5 oznacza, że wygeneruje nam 5 cyfr, w stylu 00001
    }
    
    protected abstract string GetContainerType(); //typ kontenera - C / G / L
    
    public virtual void EmptyCargo()
    {
        cargoMass = 0;
    }
    
    public virtual void LoadCargo(double loadMass)
    {
        if (loadMass > maxCapacity)
        {
            throw new OverfillException("Masa ładunku przekracza maksymalną ładowność kontenera");
        }
        cargoMass = loadMass;
    }

    public override string ToString()
    {
        return $"Numer seryjny: {serialNumber}, " +
               $"ładunek: {cargoMass} kg, " +
               $"pojemność: {maxCapacity} kg";
    }
}