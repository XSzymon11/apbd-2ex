using apbd_wc.Containers;
using apbd_wc.Interfaces_Exceptions;

namespace apbd_wc;

public static class ConsoleApp
{
    public static void Main(string[] args)
    {
        
        /*
        // Aplikacja Konsolowa
        ApplicationRunner runner = new ApplicationRunner();
        runner.RunApplication();
*/
        
        /*
        // SPRWAWDZENIE DZIAŁANIA METDO W MAINIE  
        // 1. Stworzenie kilku kontenerów (różnych typów)
        ContainerC containerC1 = new ContainerC(
            height: 200,
            ownWeight: 1000,
            depth: 150,
            maxCapacity: 2000,
            productType: "Bananas",
            temperature: 14
        );

        ContainerG containerG1 = new ContainerG(
            height: 180,
            ownWeight: 800,
            depth: 120,
            maxCapacity: 1500,
            pressure: 2.5
        );

        ContainerL containerL1 = new ContainerL(
            height: 220,
            ownWeight: 900,
            depth: 200,
            maxCapacity: 3000,
            isHazardous: true
        );

        // 2. Stworzenie statków
        Ship ship1 = new Ship("Atlantic", 25.0, 5, 50.0);
        Ship ship2 = new Ship("Pacific", 20.0, 3, 30.0);

        // 3. Załadowanie ładunku do kontenerów
        containerC1.LoadCargo(500);
        containerG1.LoadCargo(1000);
        containerL1.LoadCargo(1000);

        // 4. Załadowanie pojedynczego kontenera na statek
        ship1.LoadContainer(containerC1);

        // 5. Załadowanie listy kontenerów na statek
        List<Container> newContainers = new List<Container> {containerG1, containerL1};
        ship1.LoadContainers(newContainers);

        // 6. Usunięcie kontenera C1 ze statku
        ship1.RemoveContainer(containerC1.serialNumber);

        // 7. Rozładowanie (opróżnienie) kontenera G1 na statku
        ship1.UnloadContainer(containerG1.serialNumber);

        // 8. Zastąpienie kontenera G1 na statku nowym kontenerem C2
        ContainerC containerC2 = new ContainerC(
            height: 200,
            ownWeight: 1100,
            depth: 150,
            maxCapacity: 2000,
            productType: "Chocolate",
            temperature: 19
        );
        ship1.ReplaceContainer(containerG1.serialNumber, containerC2);

        // 9. Przeniesienie kontenera L1 z ship1 do ship2
        Ship.TransferContainer(ship1, ship2, containerL1.serialNumber);

        // 10. Wypisanie informacji o kontenerze C2
        Console.WriteLine("=== INFO O KONTENERZE (containerC2) ===");
        Console.WriteLine(containerC2);

        // 11. Wypisanie informacji o statkach
        Console.WriteLine("\n=== INFO O STATKU: ATLANTIC ===");
        Console.WriteLine(ship1);
        Console.WriteLine("Kontenery na statku Atlantic:");
        foreach (var c in ship1.Containers)
        {
            Console.WriteLine(" - " + c);
        }

        Console.WriteLine("\n=== INFO O STATKU: PACIFIC ===");
        Console.WriteLine(ship2);
        Console.WriteLine("Kontenery na statku Pacific:");
        foreach (var c in ship2.Containers)
        {
            Console.WriteLine(" - " + c);
        }
        
        // 12. Wywołanie OverfillException oraz IHazardNotifier
        Console.WriteLine("\n=== DEMONSTRACJA: OverfillException oraz IHazardNotifier ===");
        try
        { 
            // Kontener L1 jest typu L i jest niebezpieczny.
            // Dla kontenera hazardous (niebezpiecznego) dozwolony limit to 50% maxCapacity, czyli 3000 * 0.5 = 1500 kg.
            // Próba załadowania 1600 kg spowoduje wywołanie metody NotifyHazard (IHazardNotifier)
            // i rzutowanie OverfillException.
            containerL1.LoadCargo(1600);
        }
        catch (OverfillException ex)
        {
            Console.WriteLine("[WYJĄTEK] OverfillException: " + ex.Message);
        }
        
        // 13. Wywołanie IHazardNotifier przy zbyt niskiej temperaturze
        try
        {
            // Dla kontenera C2 produkt "Chocolate" wymaga minimalnie 18 stopni.
            // Ustawienie temperatury 10 stopni spowoduje wywołanie metody NotifyHazard i rzut ArgumentException.
            containerC2.SetTemperature(10);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("[WYJĄTEK] ArgumentException: " + ex.Message);
        }

        // 14. Zatrzymanie programu, by zobaczyć efekty w konsoli
        Console.WriteLine("\nWciśnij Enter, aby zakończyć...");
        Console.ReadLine();
*/
    }
}