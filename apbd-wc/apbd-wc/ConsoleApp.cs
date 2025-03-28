using apbd_wc.Containers;

namespace apbd_wc
{
    public class ApplicationRunner
    {
        private List<Ship> ships = new List<Ship>();
        private List<Container> containers = new List<Container>();

        public ApplicationRunner()
        {
            ships.Add(new Ship("SS Enterprise", 30, 10, 1000));
            ships.Add(new Ship("SS Voyager", 25, 8, 800));
        }

        public void RunApplication()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== APLIKACJA DO ZARZĄDZANIA KONTENERAMI I STATKAMI ===\n");
                Console.WriteLine("Dostępne akcje:");
                Console.WriteLine("1. Stwórz kontener");
                Console.WriteLine("2. Załaduj ładunek do kontenera");
                Console.WriteLine("3. Załaduj kontener na statek");
                Console.WriteLine("4. Załaduj listę kontenerów na statek");
                Console.WriteLine("5. Usuń kontener ze statku");
                Console.WriteLine("6. Rozładuj kontener (opróżnij ładunek)");
                Console.WriteLine("7. Zastąp kontener na statku innym");
                Console.WriteLine("8. Przenieś kontener między statkami");
                Console.WriteLine("9. Wypisz informacje o kontenerze");
                Console.WriteLine("10. Wypisz informacje o statku i jego ładunku");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        AddContainer();
                        break;
                    case "2":
                        LoadCargoInContainer();
                        break;
                    case "3":
                        LoadContainerOnShip();
                        break;
                    case "4":
                        LoadContainersOnShip();
                        break;
                    case "5":
                        RemoveContainerFromShip();
                        break;
                    case "6":
                        UnloadContainerOnShip();
                        break;
                    case "7":
                        ReplaceContainerOnShip();
                        break;
                    case "8":
                        TransferContainerBetweenShips();
                        break;
                    case "9":
                        ShowContainerInfo();
                        break;
                    case "10":
                        ShowShipInfo();
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        Pause();
                        break;
                }
            }
        }

        // 1. Stworzenie kontenera danego typu
        private void AddContainer()
        {
            Console.Clear();
            Console.WriteLine("=== STWÓRZ KONTENER ===");
            Console.WriteLine("Wybierz typ kontenera:");
            Console.WriteLine("1. Kontener chłodniczy (C)");
            Console.WriteLine("2. Kontener na gaz (G)");
            Console.WriteLine("3. Kontener na płyny (L)");
            Console.Write("Twój wybór: ");
            string typeChoice = Console.ReadLine();

            double height = ReadDouble("Podaj wysokość (cm): ");
            double ownWeight = ReadDouble("Podaj wagę własną (kg): ");
            double depth = ReadDouble("Podaj głębokość (cm): ");
            double maxCapacity = ReadDouble("Podaj maksymalną ładowność (kg): ");

            Container newContainer = null;
            switch (typeChoice)
            {
                case "1":
                    Console.Write("Podaj rodzaj produktu (np. Bananas): ");
                    string productType = Console.ReadLine();
                    double temperature = ReadDouble("Podaj temperaturę (stopni): ");
                    try
                    {
                        newContainer = new ContainerC(height, ownWeight, depth, maxCapacity, productType, temperature);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd: {ex.Message}");
                        Pause();
                        return;
                    }
                    break;
                case "2":
                    double pressure = ReadDouble("Podaj ciśnienie (atm): ");
                    newContainer = new ContainerG(height, ownWeight, depth, maxCapacity, pressure);
                    break;
                case "3":
                    Console.Write("Czy ładunek jest niebezpieczny? (true/false): ");
                    bool isHazardous = bool.Parse(Console.ReadLine() ?? "false");
                    newContainer = new ContainerL(height, ownWeight, depth, maxCapacity, isHazardous);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór typu kontenera.");
                    Pause();
                    return;
            }

            containers.Add(newContainer);
            Console.WriteLine($"Utworzono kontener: {newContainer}");
            Pause();
        }

        // 2. Załaduj ładunek do danego kontenera
        private void LoadCargoInContainer()
        {
            Console.Clear();
            Console.WriteLine("=== ZAŁADUJ ŁADUNEK DO KONTENERA ===");
            Console.Write("Podaj numer seryjny kontenera: ");
            string serial = Console.ReadLine();
            var container = containers.FirstOrDefault(c => c.serialNumber == serial);
            if (container == null)
            {
                Console.WriteLine("Nie znaleziono kontenera.");
                Pause();
                return;
            }
            double loadMass = ReadDouble("Podaj masę ładunku (kg): ");
            try
            {
                container.LoadCargo(loadMass);
                Console.WriteLine("Ładunek został załadowany do kontenera.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Pause();
        }

        // 3. Załaduj kontener na statek
        private void LoadContainerOnShip()
        {
            Console.Clear();
            Console.WriteLine("=== ZAŁADUJ KONTENER NA STATEK ===");
            Console.Write("Podaj numer seryjny kontenera: ");
            string serial = Console.ReadLine();
            var container = containers.FirstOrDefault(c => c.serialNumber == serial);
            if (container == null)
            {
                Console.WriteLine("Nie znaleziono kontenera.");
                Pause();
                return;
            }
            Console.Write("Podaj nazwę statku: ");
            string shipName = Console.ReadLine();
            var ship = ships.FirstOrDefault(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
            if (ship == null)
            {
                Console.WriteLine("Nie znaleziono statku.");
                Pause();
                return;
            }
            try
            {
                ship.LoadContainer(container);
                Console.WriteLine("Kontener został załadowany na statek.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Pause();
        }

        // 4. Załaduj listę kontenerów na statek
        private void LoadContainersOnShip()
        {
            Console.Clear();
            Console.WriteLine("=== ZAŁADUJ LISTĘ KONTENER NA STATEK ===");
            Console.Write("Podaj nazwę statku: ");
            string shipName = Console.ReadLine();
            var ship = ships.FirstOrDefault(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
            if (ship == null)
            {
                Console.WriteLine("Nie znaleziono statku.");
                Pause();
                return;
            }
            Console.Write("Podaj numery seryjne kontenerów (oddzielone przecinkami): ");
            string input = Console.ReadLine();
            var serials = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(s => s.Trim())
                               .ToList();
            List<Container> containersToLoad = new List<Container>();
            foreach (var serial in serials)
            {
                var container = containers.FirstOrDefault(c => c.serialNumber == serial);
                if (container == null)
                {
                    Console.WriteLine($"Nie znaleziono kontenera o numerze: {serial}");
                }
                else
                {
                    containersToLoad.Add(container);
                }
            }
            try
            {
                ship.LoadContainers(containersToLoad);
                Console.WriteLine("Wybrane kontenery zostały załadowane na statek.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Pause();
        }

        // 5. Usuń kontener ze statku
        private void RemoveContainerFromShip()
        {
            Console.Clear();
            Console.WriteLine("=== USUŃ KONTENER ZE STATKU ===");
            Console.Write("Podaj nazwę statku: ");
            string shipName = Console.ReadLine();
            var ship = ships.FirstOrDefault(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
            if (ship == null)
            {
                Console.WriteLine("Nie znaleziono statku.");
                Pause();
                return;
            }
            Console.Write("Podaj numer seryjny kontenera do usunięcia: ");
            string serial = Console.ReadLine();
            try
            {
                ship.RemoveContainer(serial);
                Console.WriteLine("Kontener został usunięty ze statku.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Pause();
        }

        // 6. Rozładuj kontener (opróżnij ładunek)
        private void UnloadContainerOnShip()
        {
            Console.Clear();
            Console.WriteLine("=== ROZŁADUJ KONTENER NA STATKU ===");
            Console.Write("Podaj nazwę statku: ");
            string shipName = Console.ReadLine();
            var ship = ships.FirstOrDefault(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
            if (ship == null)
            {
                Console.WriteLine("Nie znaleziono statku.");
                Pause();
                return;
            }
            Console.Write("Podaj numer seryjny kontenera do rozładunku: ");
            string serial = Console.ReadLine();
            try
            {
                ship.UnloadContainer(serial);
                Console.WriteLine("Kontener został rozładowany (ładunek opróżniony).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Pause();
        }

        // 7. Zastąp kontener na statku innym
        private void ReplaceContainerOnShip()
        {
            Console.Clear();
            Console.WriteLine("=== ZASTĄP KONTENER NA STATKU ===");
            Console.Write("Podaj nazwę statku: ");
            string shipName = Console.ReadLine();
            var ship = ships.FirstOrDefault(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
            if (ship == null)
            {
                Console.WriteLine("Nie znaleziono statku.");
                Pause();
                return;
            }
            Console.Write("Podaj numer seryjny kontenera, który chcesz zastąpić: ");
            string oldSerial = Console.ReadLine();
            Console.Write("Podaj numer seryjny nowego kontenera: ");
            string newSerial = Console.ReadLine();
            var newContainer = containers.FirstOrDefault(c => c.serialNumber == newSerial);
            if (newContainer == null)
            {
                Console.WriteLine("Nie znaleziono nowego kontenera.");
                Pause();
                return;
            }
            try
            {
                ship.ReplaceContainer(oldSerial, newContainer);
                Console.WriteLine("Kontener został zastąpiony.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Pause();
        }

        // 8. Przenieś kontener między statkami
        private void TransferContainerBetweenShips()
        {
            Console.Clear();
            Console.WriteLine("=== PRZENIEŚ KONTENER MIĘDZY STATKAMI ===");
            Console.Write("Podaj nazwę statku ŹRÓDŁOWEGO: ");
            string fromShipName = Console.ReadLine();
            var fromShip = ships.FirstOrDefault(s => s.Name.Equals(fromShipName, StringComparison.OrdinalIgnoreCase));
            if (fromShip == null)
            {
                Console.WriteLine("Nie znaleziono statku ŹRÓDŁOWEGO.");
                Pause();
                return;
            }
            Console.Write("Podaj nazwę statku DOCELOWEGO: ");
            string toShipName = Console.ReadLine();
            var toShip = ships.FirstOrDefault(s => s.Name.Equals(toShipName, StringComparison.OrdinalIgnoreCase));
            if (toShip == null)
            {
                Console.WriteLine("Nie znaleziono statku DOCELOWEGO.");
                Pause();
                return;
            }
            Console.Write("Podaj numer seryjny kontenera do przeniesienia: ");
            string serial = Console.ReadLine();
            try
            {
                Ship.TransferContainer(fromShip, toShip, serial);
                Console.WriteLine("Kontener został przeniesiony między statkami.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Pause();
        }

        // 9. Wypisz informacje o danym kontenerze
        private void ShowContainerInfo()
        {
            Console.Clear();
            Console.WriteLine("=== INFORMACJE O KONTENERZE ===");
            Console.Write("Podaj numer seryjny kontenera: ");
            string serial = Console.ReadLine();
            var container = containers.FirstOrDefault(c => c.serialNumber == serial);
            if (container == null)
            {
                Console.WriteLine("Nie znaleziono kontenera.");
            }
            else
            {
                Console.WriteLine(container.ToString());
                if (container is ContainerC cc)
                {
                    Console.WriteLine($"Produkt: {cc.ProductType}, Temperatura: {cc.Temperature}°C");
                }
                else if (container is ContainerG cg)
                {
                    Console.WriteLine($"Ciśnienie: {cg.pressure} atm");
                }
                else if (container is ContainerL cl)
                {
                    Console.WriteLine($"Ładunek niebezpieczny: {cl.IsHazardous}");
                }
            }
            Pause();
        }

        // 10. Wypisz informacje o danym statku i jego ładunku
        private void ShowShipInfo()
        {
            Console.Clear();
            Console.WriteLine("=== INFORMACJE O STATKU ===");
            Console.Write("Podaj nazwę statku: ");
            string shipName = Console.ReadLine();
            var ship = ships.FirstOrDefault(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
            if (ship == null)
            {
                Console.WriteLine("Nie znaleziono statku.");
            }
            else
            {
                Console.WriteLine(ship.ToString());
                Console.WriteLine("Załadowane kontenery:");
                if (ship.Containers.Count == 0)
                {
                    Console.WriteLine("Brak");
                }
                else
                {
                    foreach (var container in ship.Containers)
                    {
                        Console.WriteLine(container.ToString());
                    }
                }
            }
            Pause();
        }

        private double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                Console.WriteLine("Błędny format liczby. Spróbuj ponownie.");
            }
        }

        private void Pause()
        {
            Console.WriteLine("\nNaciśnij Enter, aby kontynuować...");
            Console.ReadLine();
        }
    }
}