using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Factory;
using Ex03.GarageLogic.Models.Energy_Models;
using Ex03.GarageLogic.Models.Vehicle_Models;

namespace Ex03.GarageLogic.Logic
{
    public class GarageAppLogic
    {
        private readonly List<VehicleModel> r_VehicleList = new List<VehicleModel>();
        private readonly FactoryLogic r_VehicleFactory = new FactoryLogic();

        public void ChangeVehicleStatus(string i_VehicleLicensePlateNumber, eVehicleStatus i_NewVehicleStatus)
        {
            foreach (VehicleModel vehicle in r_VehicleList)
            {
                if (vehicle.LicensePlateNumber == i_VehicleLicensePlateNumber)
                {
                    vehicle.RepairStatus = i_NewVehicleStatus;
                    break;
                }
            }
        }

        public List<string> GetLicensePlates()
        {
            List<string> licensePlates = new List<string>();

            foreach (VehicleModel vehicle in r_VehicleList)
            {
                licensePlates.Add(vehicle.LicensePlateNumber);
            }

            return licensePlates;
        }

        public string GetVehicleDetails(string i_VehicleLicensePlateNumber)
        {
            VehicleModel vehicle = getVehicle(i_VehicleLicensePlateNumber);
            string vehicleDetails = "Vehicle Not Found!";

            if (vehicle != null)
            {
                vehicleDetails = vehicle.ToString();
            }

            return vehicleDetails;
        }

        public void InflateVehicleWheelsToMaximum(string i_VehicleLicensePlateNumber)
        {
            VehicleModel vehicle = getVehicle(i_VehicleLicensePlateNumber);

            vehicle.InflateVehicleWheelsToMaximum();
        }

        public bool RechargeVehicle(string i_VehicleLicensePlateNumber, float i_AmountOfMinutesToRecharged, out string o_InstructionMsg)
        {
            VehicleModel vehicle = getVehicle(i_VehicleLicensePlateNumber);
            bool isRechargingSuccessful = false;

            o_InstructionMsg = null;

            if (vehicle.Engine.CurrentEnergyAmount == vehicle.Engine.MaxEnergyCapacity)
            {
                isRechargingSuccessful = true;
                o_InstructionMsg = "The vehicle is already fully charged.";
            }
            else
            {
                try
                {
                    isRechargingSuccessful = (vehicle.Engine as ElectricEngineModel).RechargeVehicle(i_AmountOfMinutesToRecharged);
                }
                catch (Exception ex)
                {
                    o_InstructionMsg = ex.Message;
                }
            }

            return isRechargingSuccessful;
        }

        public bool RefuelVehicle(string i_VehicleLicensePlateNumber, eEnergyType i_FuelType, float i_AmountToRefuel, out string o_InstructionMsg)
        {
            VehicleModel vehicle = getVehicle(i_VehicleLicensePlateNumber);
            bool isRefuelSuccessfully = false;

            o_InstructionMsg = null;

            if (vehicle.Engine.CurrentEnergyAmount == vehicle.Engine.MaxEnergyCapacity)
            {
                isRefuelSuccessfully = true;
                o_InstructionMsg = "The vehicle is already fully charged.";
            }
            else
            {
                try
                {
                    isRefuelSuccessfully = (vehicle.Engine as FuelEngineModel).Refuel(i_FuelType, i_AmountToRefuel);
                }
                catch (Exception ex)
                {
                    o_InstructionMsg = ex.Message;
                }
            }

            return isRefuelSuccessfully;
        }

        public bool IsVehicleInTheGarage(string i_VehicleLicensePlateNumber)
        {
            bool isVehicleInTheGarage = false;

            foreach (VehicleModel vehicle in r_VehicleList)
            {
                if (vehicle.LicensePlateNumber == i_VehicleLicensePlateNumber)
                {
                    isVehicleInTheGarage = true;
                    break;
                }
            }

            return isVehicleInTheGarage;
        }

        private VehicleModel getVehicle(string i_VehicleLicensePlateNumber)
        {
            return r_VehicleList.Find(vehicle => vehicle.LicensePlateNumber == i_VehicleLicensePlateNumber);
        }

        public List<string> GetLicensePlatesByStatus(eVehicleStatus i_RepairStatus)
        {
            List<string> licensePlates = new List<string>();

            foreach (VehicleModel vehicle in r_VehicleList)
            {
                if (vehicle.RepairStatus == i_RepairStatus)
                {
                    licensePlates.Add(vehicle.LicensePlateNumber);
                }
            }

            return licensePlates;
        }

        private VehicleModel GetVehicle(string i_LicensePlateNumber)
        {
            return r_VehicleList.Find(vehicle => vehicle.LicensePlateNumber == i_LicensePlateNumber);
        }

        public List<string> SupportedVehicles()
        {
            List<string> supportedVehicles = new List<string>();

            foreach (VehicleModel vehicle in r_VehicleFactory.SupportedVehicles)
            {
                supportedVehicles.Add(vehicle.GetTypeOfVehicle());
            }

            return supportedVehicles;
        }

        public Dictionary<string, List<string>> GenerateVehicle(int i_VehicleNumber)
        {
            VehicleModel vehicle = null;

            switch (i_VehicleNumber)
            {
                case 1:
                    vehicle = r_VehicleFactory.CreateFuelMotorcycle();
                    break;
                case 2:
                    vehicle = r_VehicleFactory.CreateElectricMotorcycle();
                    break;
                case 3:
                    vehicle = r_VehicleFactory.CreateFuelCar();
                    break;
                case 4:
                    vehicle = r_VehicleFactory.CreateElectricCar();
                    break;
                case 5:
                    vehicle = r_VehicleFactory.CreateTruck();
                    break;
                default:
                    throw new ValueOutOfRangeException(1, r_VehicleFactory.SupportedVehicles.Count());
            }

            r_VehicleList.Add(vehicle);
            r_VehicleFactory.CurrentVehicle = vehicle;

            return vehicle.Properties;
        }

        public void UpdateLicensePlate(string i_LicensePlate)
        {
            r_VehicleFactory.CurrentVehicle.LicensePlateNumber = i_LicensePlate;
        }

        public bool UpdateCarDetails(string i_VehicleProperty, out string o_InstructionMsg)
        {
            bool isUpdated = false;

            o_InstructionMsg = null;

            try
            {
                r_VehicleFactory.CurrentVehicle.UpdateVehicleDetails(i_VehicleProperty);
                isUpdated = true;
            }
            catch (Exception ex)
            {
                Dictionary<string, List<string>> properties = r_VehicleFactory.CurrentVehicle.Properties;

                ResetChoice(properties, i_VehicleProperty);
                o_InstructionMsg = ex.Message;
                isUpdated = false;
            }

            return isUpdated;
        }

        public void ResetChoice(Dictionary<string, List<string>> i_VehicleProperties, string i_Key)
        {
            i_VehicleProperties[i_Key].RemoveAt(i_VehicleProperties[i_Key].Count - 1);
        }

        public bool IsVehicleElectric(string i_UserInputLicensePlateNumber)
        {
            VehicleModel vehicle = GetVehicle(i_UserInputLicensePlateNumber);

            return vehicle != null && vehicle.Engine.EnergyType == eEnergyType.Electric;
        }
    }
}