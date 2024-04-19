using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Models.Energy_Models;
using Ex03.GarageLogic.Models.Vehicle_Models;

namespace Ex03.GarageLogic.Factory
{
    internal class FactoryLogic
    {
        private const int k_NumberOfSupportedVehicles = 5;
        internal List<VehicleModel> SupportedVehicles { get; private set; } = null;
        internal VehicleModel CurrentVehicle = null;

        internal FactoryLogic()
        {
            GenerateSupportedVehiclesList();
        }

        private void GenerateSupportedVehiclesList()
        {
            MotorcycleModel fuelMotorcycle = new MotorcycleModel();
            MotorcycleModel electricMotorcycle = new MotorcycleModel();
            CarModel fuelCar = new CarModel();
            CarModel electricCar = new CarModel();
            TruckModel fuelTruck = new TruckModel();

            fuelMotorcycle.Engine = new FuelEngineModel();
            fuelMotorcycle.Engine.SetEngineType(eEnergyType.Octan98, 5.8f, 5.8f);
            electricMotorcycle.Engine = new ElectricEngineModel();
            electricMotorcycle.Engine.SetEngineType(eEnergyType.Electric, 2.8f, 2.8f);
            fuelCar.Engine = new FuelEngineModel();
            fuelCar.Engine.SetEngineType(eEnergyType.Octan95, 58f, 58f);
            electricCar.Engine = new ElectricEngineModel();
            electricCar.Engine.SetEngineType(eEnergyType.Electric, 4.8f, 4.8f);
            fuelTruck.Engine = new FuelEngineModel();
            fuelTruck.Engine.SetEngineType(eEnergyType.Soler, 110f, 110f);
            SupportedVehicles = new List<VehicleModel>() { fuelMotorcycle, electricMotorcycle, fuelCar, electricCar, fuelTruck };
        }

        internal MotorcycleModel CreateFuelMotorcycle()
        {
            MotorcycleModel fuelMotorcycle = new MotorcycleModel();

            fuelMotorcycle.Engine = new FuelEngineModel();
            fuelMotorcycle.Engine.SetEngineType(eEnergyType.Octan98, 5.8f, 5.8f);

            return fuelMotorcycle;
        }

        internal MotorcycleModel CreateElectricMotorcycle()
        {
            MotorcycleModel electricMotorcycle = new MotorcycleModel();

            electricMotorcycle.Engine = new ElectricEngineModel();
            electricMotorcycle.Engine.SetEngineType(eEnergyType.Electric, 2.8f, 2.8f);

            return electricMotorcycle;
        }

        internal CarModel CreateFuelCar()
        {
            CarModel fuelCar = new CarModel();

            fuelCar.Engine = new FuelEngineModel();
            fuelCar.Engine.SetEngineType(eEnergyType.Octan95, 58f, 58f);

            return fuelCar;
        }

        internal CarModel CreateElectricCar()
        {
            CarModel electricCar = new CarModel();

            electricCar.Engine = new ElectricEngineModel();
            electricCar.Engine.SetEngineType(eEnergyType.Electric, 4.8f, 4.8f);

            return electricCar;
        }

        internal TruckModel CreateTruck()
        {
            TruckModel truck = new TruckModel();

            truck.Engine = new FuelEngineModel();
            truck.Engine.SetEngineType(eEnergyType.Soler, 110f, 110f);

            return truck;
        }
    }
}