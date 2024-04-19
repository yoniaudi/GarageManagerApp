using Ex03.ConsoleUI;

public static class Program
{
    public static void Main()
    {
        StartGarageApp();
    }

    private static void StartGarageApp()
    {
        GarageUI garageUI = new GarageUI();

        garageUI.StartGarageApp();
    }
}