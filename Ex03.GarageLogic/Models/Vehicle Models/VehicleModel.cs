using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Models.Energy_Models;
using System.Text;

namespace Ex03.GarageLogic.Models.Vehicle_Models
{
    internal abstract class VehicleModel
    {
        internal WheelModel[] Wheels { get; set; } = null;
        internal EngineModel Engine { get; set; } = null;
        internal eVehicleStatus RepairStatus { get; set; } = eVehicleStatus.InRepair;
        internal Dictionary<string, List<string>> Properties { get; private set; } = null;
        private string m_Model = null;
        private string m_OwnerName = null;
        private string m_OwnerPhoneNumber = null;
        private string m_LicensePlateNumber = null;

        internal string Model
        {
            get { return m_Model; }
            set
            {
                if (value.Length == 0)
                {
                    throw new FormatException("Invalid input, enter a vehicle model name.");
                }

                m_Model = value;
            }
        }

        internal string OwnerName
        {
            get { return m_OwnerName; }
            set
            {
                if (value.Length == 0)
                {
                    throw new FormatException("Invalid input, enter a customer name.");
                }

                m_OwnerName = value;
            }
        }

        internal string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set
            {
                if (value.Length == 0)
                {
                    throw new FormatException("Invalid input, enter a phone number.");
                }

                m_OwnerPhoneNumber = value;
            }
        }

        internal string LicensePlateNumber
        {
            get { return m_LicensePlateNumber; }
            set
            {
                if (value.Length == 0)
                {
                    throw new FormatException("Invalid input, enter a license plate number.");
                }

                m_LicensePlateNumber = value;
            }
        }

        internal virtual void CreateMenu()
        {
            Properties = new Dictionary<string, List<string>>()
            {
                { "Owner", new List<string>() },
                { "Phone", new List<string>() },
                { "Model", new List<string>() },
                { "Wheel manufecturer", new List<string>() },
                { "Wheel air pressure", new List<string>() },
                { "Energy amount", new List<string>() }
            };
        }

        internal virtual void UpdateVehicleDetails(string i_Key)
        {
            switch (i_Key)
            {
                case "License plate":
                    LicensePlateNumber = Properties[i_Key].Last();
                    break;
                case "Owner":
                    OwnerName = Properties[i_Key].Last();
                    break;
                case "Phone":
                    updatePhoneNumber(i_Key);
                    break;
                case "Model":
                    Model = Properties[i_Key].Last();
                    break;
                case "Wheel manufecturer":
                    updateWheelsManufecturer(i_Key);
                    break;
                case "Wheel air pressure":
                    updateWheelsAirPressure(i_Key);
                    break;
                case "Energy amount":
                    updateEnergyAmount(i_Key);
                    break;
            }
        }

        private void updatePhoneNumber(string i_Key)
        {
            bool isPhoneFormatValid = Properties[i_Key].Last().All(char.IsDigit);

            if (isPhoneFormatValid == false)
            {
                throw new FormatException("Invalid input, please use only digits.");
            }

            OwnerPhoneNumber = Properties[i_Key].Last();
        }

        private void updateWheelsManufecturer(string i_Key)
        {
            if (Properties[i_Key].Last().Length == 0)
            {
                throw new FormatException("Invalid input, enter a wheel manufecturer.");
            }

            foreach (WheelModel wheel in Wheels)
            {
                wheel.Manufecturer = Properties[i_Key].Last();
            }
        }

        private void updateWheelsAirPressure(string i_Key)
        {
            float currentAirPressure = 0f;
            bool isParsedSuccessfully = Single.TryParse(Properties[i_Key].Last(), out currentAirPressure);

            if (isParsedSuccessfully == false)
            {
                throw new FormatException("Invalid input, value has to be float.");
            }

            foreach (WheelModel wheel in Wheels)
            {
                wheel.CurrentAirPressure = currentAirPressure;
            }
        }

        private void updateEnergyAmount(string i_Key)
        {
            float currentEnergyAmount = 0f;
            bool isParsedSuccessfully = Single.TryParse(Properties[i_Key].Last(), out currentEnergyAmount);

            if (isParsedSuccessfully == false)
            {
                throw new FormatException("Invalid input, value has to be float");
            }

            Engine.CurrentEnergyAmount = currentEnergyAmount;
        }

        internal void InflateVehicleWheelsToMaximum()
        {
            foreach (WheelModel wheel in Wheels)
            {
                float desiredAirPressure = wheel.MaxAirPressure - wheel.CurrentAirPressure;

                wheel.InflateVehicleWheels(desiredAirPressure);
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.AppendLine($"Type: {GetTypeOfVehicle()}");
            vehicleInfo.AppendLine($"License Plate Number: {LicensePlateNumber}");
            vehicleInfo.AppendLine($"Model: {Model}");
            vehicleInfo.AppendLine($"Owner: {OwnerName}");
            vehicleInfo.AppendLine($"Phone Number: {OwnerPhoneNumber}");
            vehicleInfo.AppendLine($"Status: {RepairStatus}");
            vehicleInfo.AppendLine("Wheels: ");

            foreach (WheelModel wheel in Wheels)
            {
                vehicleInfo.AppendLine(wheel.ToString());
            }

            vehicleInfo.AppendLine(Engine.ToString());

            return vehicleInfo.ToString();
        }

        internal abstract string GetTypeOfVehicle();
    }
}
