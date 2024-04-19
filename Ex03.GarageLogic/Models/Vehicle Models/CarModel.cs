using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Models.Vehicle_Models
{
    internal class CarModel : VehicleModel
    {
        private const int k_NumberOfWheels = 5;
        private const int k_MaxNumberOfDoors = 5;
        private const int k_MinNumberOfDoors = 2;
        private const float k_MaxWheelAirPressure = 30f;
        private eCarColor m_Color = new eCarColor();
        private int m_NumberOfDoors = 0;

        internal int NumberOfDoors
        {
            set
            {
                if (value < k_MinNumberOfDoors || value > k_MaxNumberOfDoors)
                {
                    throw new ValueOutOfRangeException(k_MaxNumberOfDoors, k_MinNumberOfDoors);
                }

                m_NumberOfDoors = value;
            }
        }

        internal CarModel()
        {
            Wheels = new WheelModel[k_NumberOfWheels];

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                WheelModel wheel = new WheelModel(k_MaxWheelAirPressure);
                wheel.InflateVehicleWheels(k_MaxWheelAirPressure);
                Wheels[i] = wheel;
            }

            CreateMenu();
        }

        internal override void CreateMenu()
        {
            base.CreateMenu();
            Properties.Add("Color", new List<string>() { "1. Blue", "2. White", "3. Red", "4. Yellow" });
            Properties.Add("Number of doors", new List<string>() { "1. Two doors", "2. Three doors", "3. Four doors", "4. Five doors" });
        }

        internal override void UpdateVehicleDetails(string i_Key)
        {
            switch (i_Key)
            {
                case "Color":
                    UpdateColor(i_Key);
                    break;
                case "Number of doors":
                    NumberOfDoors = int.Parse(Properties[i_Key].Last()) + 1;
                    break;
                default:
                    base.UpdateVehicleDetails(i_Key);
                    break;
            }
        }

        private void UpdateColor(string i_Key)
        {
            switch (Properties[i_Key].Last())
            {
                case "1":
                    m_Color = eCarColor.Blue;
                    break;
                case "2":
                    m_Color = eCarColor.White;
                    break;
                case "3":
                    m_Color = eCarColor.Red;
                    break;
                case "4":
                    m_Color = eCarColor.Yellow;
                    break;
                default:
                    throw new FormatException("Invalid input, please try again.");
            }
        }

        internal override string GetTypeOfVehicle()
        {
            string typeOfVehicle = null;

            if (Engine.EnergyType == eEnergyType.Electric)
            {
                typeOfVehicle = "Electric Car";
            }
            else
            {
                typeOfVehicle = "Fuel Car";
            }

            return typeOfVehicle;
        }

        public override string ToString()
        {
            string carDetailsMsg = string.Format("Color: {0}{1}Doors: {2}",
                m_Color, Environment.NewLine, m_NumberOfDoors);

            return base.ToString() + carDetailsMsg;
        }
    }
}