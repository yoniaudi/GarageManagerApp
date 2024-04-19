using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Logic;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        private readonly GarageAppLogic r_GarageLogic = new GarageAppLogic();

        internal void StartGarageApp()
        {
            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();

            while (UserInput.Key != ConsoleKey.D8)
            {
                displayMenu();
                UserInput = Console.ReadKey();
                clearCurrentLine();

                switch (UserInput.Key)
                {
                    case ConsoleKey.D1:
                        addNewVehicleToGarage();
                        break;
                    case ConsoleKey.D2:
                        displayListOfVehiclesInGarage();
                        break;
                    case ConsoleKey.D3:
                        changeVehicleStatus();
                        break;
                    case ConsoleKey.D4:
                        inflateVehicleWheelsToMaximum();
                        break;
                    case ConsoleKey.D5:
                        refuelVehicle();
                        break;
                    case ConsoleKey.D6:
                        rechargeVehicle();
                        break;
                    case ConsoleKey.D7:
                        displayVehicleDetails();
                        break;
                    case ConsoleKey.D8:
                        Console.WriteLine();
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid input, please try again.");
                        break;
                }
            }
        }

        private void refuelVehicle()
        {
            bool isElectric = false;

            operateEnergyVehicle(isElectric);
        }

        private void rechargeVehicle()
        {
            bool isElectric = true;

            operateEnergyVehicle(isElectric);
        }

        private void operateEnergyVehicle(bool i_IsElectric)
        {
            string instructionMsg = null;
            string actionName = i_IsElectric ? "recharge" : "refuel";
            string userInputLicensePlateNumber = getLicensePlateNumberFromUser();
            bool isVehicleExists = r_GarageLogic.IsVehicleInTheGarage(userInputLicensePlateNumber);
            bool isVehicleElectric = r_GarageLogic.IsVehicleElectric(userInputLicensePlateNumber);
            bool isOperationSuccessfully = false;

            if (isVehicleExists == true)
            {
                if ((isVehicleElectric == true && i_IsElectric == true) || (isVehicleElectric == false && i_IsElectric == false))
                {
                    while (isOperationSuccessfully == false)
                    {
                        eEnergyType energyType = new eEnergyType();
                        float amountToFill = 0;

                        if (i_IsElectric == false)
                        {
                            energyType = getValidFuelTypeFromUser();
                        }

                        amountToFill = getAmountOfEnergyToFillUpFromUser();
                        isOperationSuccessfully = i_IsElectric == true ?
                            r_GarageLogic.RechargeVehicle(userInputLicensePlateNumber, amountToFill, out instructionMsg) :
                            r_GarageLogic.RefuelVehicle(userInputLicensePlateNumber, energyType, amountToFill, out instructionMsg);

                        if (isOperationSuccessfully == false)
                        {
                            Console.WriteLine(instructionMsg);
                            instructionMsg = null;
                        }
                    }

                    if (isOperationSuccessfully == true && instructionMsg != null)
                    {
                        Console.WriteLine(instructionMsg);
                    }
                    else
                    {
                        instructionMsg = $"The vehicle {actionName} successfully.";
                        Console.WriteLine(instructionMsg);
                    }
                }
                else
                {
                    instructionMsg = $"This is {(isVehicleElectric == true ? "an electric" : "a fuel")} engine, this operation cannot be performed.";
                    Console.WriteLine(instructionMsg);
                }
            }
            else
            {
                Console.WriteLine("The vehicle is not in the garage.");
            }

            Console.WriteLine();
            goBackMessage();
        }

        private float getAmountOfEnergyToFillUpFromUser()
        {
            float energyAmount = 0f;
            string userInput = null;
            bool isUserInputValid = false;

            do
            {
                Console.WriteLine("Enter an amount to fill up: ");
                userInput = Console.ReadLine();
                isUserInputValid = Single.TryParse(userInput, out energyAmount);

                if (isUserInputValid == false || energyAmount < 0)
                {
                    Console.WriteLine("Invalid input, please enter a positive number.");
                }

                Console.WriteLine();
            }
            while (isUserInputValid == false || energyAmount < 0);

            return energyAmount;
        }

        private string getLicensePlateNumberFromUser()
        {
            string userInput = null;

            Console.Write("Enter a license plate number: ");
            userInput = Console.ReadLine();
            Console.WriteLine();

            while (userInput.Length == 0)
            {
                Console.WriteLine("Invalid input, please enter a license plate number.");
                Console.WriteLine();
                userInput = getLicensePlateNumberFromUser();
            }

            return userInput;
        }

        private void inflateVehicleWheelsToMaximum()
        {
            string userInputLicensePlateNumber = getLicensePlateNumberFromUser();
            bool isVehicleExists = r_GarageLogic.IsVehicleInTheGarage(userInputLicensePlateNumber);

            if (isVehicleExists == true)
            {
                r_GarageLogic.InflateVehicleWheelsToMaximum(userInputLicensePlateNumber);
                Console.WriteLine("The vehicle's wheels were inflated to the maximum.");
            }
            else
            {
                Console.WriteLine("The vehicle is not in the garage.");
            }

            Console.WriteLine();
            goBackMessage();
        }

        private void changeVehicleStatus()
        {
            string userInputLicensePlateNumber = getLicensePlateNumberFromUser();
            bool isVehicleExists = r_GarageLogic.IsVehicleInTheGarage(userInputLicensePlateNumber);

            if (isVehicleExists == true)
            {
                eVehicleStatus newRepairStatus = getNewRepairStatusFromUserInput();

                r_GarageLogic.ChangeVehicleStatus(userInputLicensePlateNumber, newRepairStatus);
                Console.WriteLine("The vehicle's status changed successfully.");
            }
            else
            {
                Console.WriteLine("The vehicle is not in the garage.");
            }

            Console.WriteLine();
        }

        private eVehicleStatus getNewRepairStatusFromUserInput()
        {
            ConsoleKeyInfo userInput = new ConsoleKeyInfo();
            eVehicleStatus newVehicleStatus = new eVehicleStatus();
            string newStatusMessage = string.Format("Choose new vehicle status:{0}1. In Repair{0}2. Repaired{0}3. Paid", Environment.NewLine);

            do
            {
                Console.WriteLine(newStatusMessage);
                userInput = Console.ReadKey();
                clearCurrentLine();
                Console.WriteLine();

                switch (userInput.Key)
                {
                    case ConsoleKey.D1:
                        newVehicleStatus = eVehicleStatus.InRepair;
                        break;
                    case ConsoleKey.D2:
                        newVehicleStatus = eVehicleStatus.Repaired;
                        break;
                    case ConsoleKey.D3:
                        newVehicleStatus = eVehicleStatus.Paid;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid input, please try again.");
                        break;
                }
            }
            while (userInput.Key != ConsoleKey.D1 && userInput.Key != ConsoleKey.D2 && userInput.Key != ConsoleKey.D3);

            return newVehicleStatus;
        }

        private void addNewVehicleToGarage()
        {
            string vehicleLicensePlate = getLicensePlateNumberFromUser();
            bool isVehicleExists = r_GarageLogic.IsVehicleInTheGarage(vehicleLicensePlate);
            bool isUpdated = false;

            if (isVehicleExists == true)
            {
                r_GarageLogic.ChangeVehicleStatus(vehicleLicensePlate, eVehicleStatus.InRepair);
                Console.WriteLine("The vehicle is already in the garage.");
                Console.WriteLine();
                goBackMessage();
            }
            else
            {
                int vehicleNumber = chooseSupportedVehicle();
                Dictionary<string, List<string>> vehicleProperties = r_GarageLogic.GenerateVehicle(vehicleNumber);

                r_GarageLogic.UpdateLicensePlate(vehicleLicensePlate);

                foreach (string propertyName in vehicleProperties.Keys)
                {
                    while (isUpdated == false)
                    {
                        Console.Write(propertyName);
                        Console.WriteLine(": ");

                        if (vehicleProperties[propertyName].Count() == 0)
                        {
                            string userInputStr = Console.ReadLine();

                            Console.WriteLine();
                            vehicleProperties[propertyName].Add(userInputStr);
                        }
                        else
                        {
                            ConsoleKeyInfo userInput = new ConsoleKeyInfo();

                            vehicleProperties[propertyName].ForEach(Console.WriteLine);
                            userInput = Console.ReadKey();
                            clearCurrentLine();
                            Console.WriteLine();

                            while (userInput.KeyChar - '0' < 1 || userInput.KeyChar - '0' > vehicleProperties[propertyName].Count())
                            {
                                Console.WriteLine("Invalid input, please try again.");
                                Console.WriteLine();
                                userInput = Console.ReadKey();
                                clearCurrentLine();
                            }

                            vehicleProperties[propertyName].Add(userInput.KeyChar.ToString());
                        }

                        isUpdated = r_GarageLogic.UpdateCarDetails(propertyName, out string errorMsg);

                        if (isUpdated == false)
                        {
                            Console.WriteLine(errorMsg);
                            Console.WriteLine("Invalid input, please try again.");
                            Console.WriteLine();
                        }
                    }

                    isUpdated = false;
                }
            }
        }

        private int chooseSupportedVehicle()
        {
            int vehicleNumber = 1;
            ConsoleKeyInfo userChoice = new ConsoleKeyInfo();
            List<string> supportedVehicles = r_GarageLogic.SupportedVehicles();

            Console.WriteLine("The vehicle is not in the garage.");
            Console.WriteLine("Choose the vehicle you would like to add:");
            Console.WriteLine();

            foreach (string vehicle in supportedVehicles)
            {
                string vehicleToDisplay = string.Format("{0}. {1}", vehicleNumber++, vehicle);

                Console.WriteLine(vehicleToDisplay);
            }

            Console.WriteLine();
            userChoice = Console.ReadKey();
            clearCurrentLine();

            while (userChoice.KeyChar - '0' < 1 || userChoice.KeyChar - '0' > supportedVehicles.Count())
            {
                Console.WriteLine("Invalid input, please try again.");
                userChoice = Console.ReadKey();
                clearCurrentLine();
            }

            return userChoice.KeyChar - '0';
        }

        private eEnergyType getValidFuelTypeFromUser()
        {
            ConsoleKeyInfo userInput = new ConsoleKeyInfo();
            eEnergyType fuelType = new eEnergyType();
            string chooseFuelTypeMessage = string.Format("Choose a fuel type:{0}1. Soler{0}2. Octan95{0}3. Octan98", Environment.NewLine);

            do
            {
                Console.WriteLine(chooseFuelTypeMessage);
                userInput = Console.ReadKey();
                clearCurrentLine();
                Console.WriteLine();

                switch (userInput.Key)
                {
                    case ConsoleKey.D1:
                        fuelType = eEnergyType.Soler;
                        break;
                    case ConsoleKey.D2:
                        fuelType = eEnergyType.Octan95;
                        break;
                    case ConsoleKey.D3:
                        fuelType = eEnergyType.Octan98;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid input, try again.");
                        break;
                }
            }
            while (userInput.Key != ConsoleKey.D1 && userInput.Key != ConsoleKey.D2 && userInput.Key != ConsoleKey.D3);

            return fuelType;
        }

        private void displayListOfVehiclesInGarage()
        {
            ConsoleKeyInfo userInput = new ConsoleKeyInfo();
            List<string> licensePlates = r_GarageLogic.GetLicensePlates();
            string filterMsg = string.Format("Filter result by:{0}1. In repair{0}2. Repaired{0}3. Paid{0}4. Back{0}",
                Environment.NewLine);

            if (licensePlates.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage.");
                Console.WriteLine();
            }
            else
            {
                do
                {
                    Console.WriteLine("Search result:");
                    licensePlates.ForEach(Console.WriteLine);
                    Console.WriteLine();
                    Console.WriteLine(filterMsg);
                    userInput = Console.ReadKey();
                    clearCurrentLine();

                    switch (userInput.Key)
                    {
                        case ConsoleKey.D1:
                            licensePlates = r_GarageLogic.GetLicensePlatesByStatus(eVehicleStatus.InRepair);
                            break;
                        case ConsoleKey.D2:
                            licensePlates = r_GarageLogic.GetLicensePlatesByStatus(eVehicleStatus.Repaired);
                            break;
                        case ConsoleKey.D3:
                            licensePlates = r_GarageLogic.GetLicensePlatesByStatus(eVehicleStatus.Paid);
                            break;
                        case ConsoleKey.D4:
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            Console.WriteLine();
                            break;
                    }
                }
                while (userInput.Key != ConsoleKey.D4);
            }
        }

        private void displayVehicleDetails()
        {
            string vehiclePlateNumber = getLicensePlateNumberFromUser();
            bool isVehicleExists = r_GarageLogic.IsVehicleInTheGarage(vehiclePlateNumber);

            if (isVehicleExists == true)
            {
                string vehicleDetails = r_GarageLogic.GetVehicleDetails(vehiclePlateNumber);

                Console.WriteLine(vehicleDetails);
            }
            else
            {
                Console.WriteLine("The vehicle is not in the garage.");
            }

            Console.WriteLine();
            goBackMessage();
        }

        private void goBackMessage()
        {
            ConsoleKeyInfo userInput = new ConsoleKeyInfo();

            do
            {
                Console.WriteLine("1. Back");
                userInput = Console.ReadKey();
                clearCurrentLine();
                Console.WriteLine();
            }
            while (userInput.Key != ConsoleKey.D1);
        }

        private void clearCurrentLine()
        {
            Console.CursorLeft = 0;
            Console.Write(new string(' ', Console.BufferWidth));
            Console.CursorLeft = 0;
        }

        private void displayMenu()
        {
            string menu = string.Format
(
@"Main Menu

1. Add A Vehicle
2. List Vehicles
3. Change A Status
4. Inflate Wheels
5. Refuel A Vehicle
6. Charge A Vehicle
7. View Vehicle's Information
8. Exit App
"
);

            Console.WriteLine(menu);
        }
    }
}