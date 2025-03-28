using apbd_wc.Containers;

namespace apbd_wc
{
    public class Ship
    {
        public string Name { get; protected set; }
        public double MaxSpeed { get; protected set; }
        public int MaxContainerCount { get; protected set; }
        public double MaxWeightInTons { get; protected set; }
        private List<Container> containers = new List<Container>();

        public Ship(string name, double maxSpeed, int maxContainerCount, double maxWeightInTons)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxWeightInTons = maxWeightInTons;
        }
        
        public IReadOnlyList<Container> Containers => containers.AsReadOnly();
        
        public void LoadContainer(Container container)
        {
            if (containers.Count >= MaxContainerCount)
            {
                throw new Exception($"Statek {Name} nie może załadować więcej kontenerów (przekroczono limit {MaxContainerCount}).");
            }
            double currentWeightInTons = containers.Sum(c => c.ownWeight + c.cargoMass) / 1000.0;
            double newWeight = (container.ownWeight + container.cargoMass) / 1000.0;
            if (currentWeightInTons + newWeight > MaxWeightInTons)
            {
                throw new Exception($"Statek {Name} przekracza maksymalną wagę {MaxWeightInTons} t.");
            }
            containers.Add(container);
        }
        
        public void LoadContainers(IEnumerable<Container> containerList)
        {
            foreach (var container in containerList)
            {
                LoadContainer(container);
            }
        }
        
        public void RemoveContainer(string serialNumber)
        {
            var container = containers.FirstOrDefault(c => c.serialNumber == serialNumber);
            if (container == null)
            {
                throw new Exception($"Kontener {serialNumber} nie znajduje się na statku {Name}");
            }
            containers.Remove(container);
        }
        
        public void UnloadContainer(string serialNumber)
        {
            var container = containers.FirstOrDefault(c => c.serialNumber == serialNumber);
            if (container == null)
            {
                throw new Exception($"Kontener {serialNumber} nie znajduje się na statku {Name}");
            }
            container.EmptyCargo();
        }
        
        public void ReplaceContainer(string oldSerialNumber, Container newContainer)
        {
            var oldContainer = containers.FirstOrDefault(c => c.serialNumber == oldSerialNumber);
            if (oldContainer == null)
            {
                throw new Exception($"Kontener {oldSerialNumber} nie znajduje się na statku {Name}");
            }
            containers.Remove(oldContainer);
            try
            {
                LoadContainer(newContainer);
            }
            catch (Exception ex)
            {
                containers.Add(oldContainer);
                throw new Exception($"Nie udało się zastąpić kontenera {oldSerialNumber}. Powód: {ex.Message}");
            }
        }
        
        public static void TransferContainer(Ship fromShip, Ship toShip, string serialNumber)
        {
            var container = fromShip.containers.FirstOrDefault(c => c.serialNumber == serialNumber);
            if (container == null)
            {
                throw new Exception($"Kontener {serialNumber} nie znajduje się na statku {fromShip.Name}");
            }
            toShip.LoadContainer(container);
            fromShip.containers.Remove(container);
        }

        public override string ToString()
        {
            return $"Statek: {Name}, prędkość: {MaxSpeed} węzłów, limit kontenerów: {MaxContainerCount}, maksymalna waga: {MaxWeightInTons} t, załadowanych kontenerów: {containers.Count}";
        }
    }
}