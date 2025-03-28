using apbd_wc.Interfaces_Exceptions;

namespace apbd_wc.Containers
{
    public class ContainerC : Container, IHazardNotifier
    {
        private static readonly Dictionary<string, double> RequiredTemperatures = new Dictionary<string, double>
        {
            { "Bananas", 13.3 },
            { "Chocolate", 18 },
            { "Fish", 2 },
            { "Meat", -15 },
            { "Ice cream", -18 },
            { "Frozen pizza", -30 },
            { "Cheese", 7.2 },
            { "Sausages", 5 },
            { "Butter", 20.5 },
            { "Eggs", 19 }
        };
        
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }

        public ContainerC(double height, double ownWeight, double depth, double maxCapacity, string productType, double temperature)
            : base(height, ownWeight, depth, maxCapacity)
        {
            if (!RequiredTemperatures.ContainsKey(productType))
            {
                NotifyHazard($"Nieznany produkt: {productType}. Brak wymagań temperaturowych w systemie");
                throw new ArgumentException($"Produkt '{productType}' nie jest obsługiwany w kontenerach chłodniczych");
            }
            
            ProductType = productType;
            
            double requiredTemp = RequiredTemperatures[productType];
            if (temperature < requiredTemp)
            {
                NotifyHazard($"Kontener {serialNumber}: " +
                             $"temperatura {temperature} stopni jest niższa niż wymagana {requiredTemp} stopni dla produktu {productType}");
                throw new ArgumentException("Temperatura kontenera jest zbyt niska dla wybranego produktu");
            }

            Temperature = temperature;
        }

        protected override string GetContainerType() => "C";
        
        public void NotifyHazard(string message)
        {
            Console.WriteLine($"[HAZARD - C] {message}");
        }

        public void SetTemperature(double newTemperature)
        {
            double requiredTemp = RequiredTemperatures[ProductType];
            if (newTemperature < requiredTemp)
            {
                NotifyHazard($"Kontener {serialNumber}: " +
                             $"temperatura {newTemperature} stopni jest niższa niż wymagana {requiredTemp} stopni" +
                             $"dla produktu {ProductType}.");
                throw new ArgumentException("Temperatura kontenera jest zbyt niska dla wybranego produktu.");
            }

            Temperature = newTemperature;
        }
    }
}