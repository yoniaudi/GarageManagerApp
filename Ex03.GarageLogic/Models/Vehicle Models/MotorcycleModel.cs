using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Models.Vehicle_Models
{
    internal class MotorcycleModel : VehicleModel
    {
        private const int k_NumberOfWheels = 2;
        private const float k_MaxWheelAirPressure = 29f;
        private eDriverLicenseType m_DriverLicenseType = new eDriverLicenseType();
        private int m_EngineCC = 0;

        internal int EngineCC
        {
            set
            {
                if (value < 0)
                {
                    throw new ValueOutOfRangeException("Invalid input, has to be a positive number.");
                }

                m_EngineCC = value;
            }
        }

        internal MotorcycleModel()
        {
            Wheels = new WheelModel[k_NumberOfWheels];

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                WheelModel wheel = new WheelModel(k_MaxWheelAirPressure);
                Wheels[i] = wheel;
            }

            CreateMenu();
        }

        internal override void CreateMenu()
        {
            base.CreateMenu();
            Properties.Add("Engine CC", new List<string>());
            Properties.Add("License Type", new List<string>() { "1. A1", "2. A2", "3. AB", "4. B2" });
        }

        internal override void UpdateVehicleDetails(string i_Key)
        {
            switch (i_Key)
            {
                case "Engine CC":
                    UpdateEngineCC(i_Key);
                    break;
                case "License Type":
                    UpdateLicenseType(i_Key);
                    break;
                default:
                    base.UpdateVehicleDetails(i_Key);
                    break;
            }
        }

        private void UpdateLicenseType(string i_Key)
        {
            switch (Properties[i_Key].Last())
            {
                case "1":
                    m_DriverLicenseType = eDriverLicenseType.A1;
                    break;
                case "2":
                    m_DriverLicenseType = eDriverLicenseType.A2;
                    break;
                case "3":
                    m_DriverLicenseType = eDriverLicenseType.AB;
                    break;
                case "4":
                    m_DriverLicenseType = eDriverLicenseType.B2;
                    break;
                default:
                    throw new FormatException("Invalid input, please try again.");
            }
        }

        public void UpdateEngineCC(string i_Key)
        {
            int engineCC = 0;
            bool isParsedSuccessfully = int.TryParse(Properties["Engine CC"].Last(), out engineCC);

            if (isParsedSuccessfully == false)
            {
                throw new FormatException("Invalid input, value needs to be an integer.");
            }

            EngineCC = engineCC;
        }

        internal override string GetTypeOfVehicle()
        {
            string typeOfVehicle = null;

            if (Engine.EnergyType == eEnergyType.Electric)
            {
                typeOfVehicle = "Electric Motorcycle";
            }
            else
            {
                typeOfVehicle = "Fuel Motorcycle";
            }

            return typeOfVehicle;
        }

        public override string ToString()
        {
            string motorcycleMsg = string.Format("License Type: {0}{1}Engine CC: {2}",
                m_DriverLicenseType, Environment.NewLine, m_EngineCC);

            return base.ToString() + motorcycleMsg;
        }
    }
}