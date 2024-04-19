using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Models.Vehicle_Models
{
    internal class TruckModel : VehicleModel
    {
        private const int k_NumberOfWheels = 12;
        private const float k_MaxWheelAirPressure = 28f;
        private float m_CabinCapacity = 0f;
        private bool m_IsTransportingDangerousGoods = false;

        internal float CabinCapacity
        {
            set
            {
                if (value < 0)
                {
                    throw new ValueOutOfRangeException("Invalid input, has to be a positive number.");
                }

                m_CabinCapacity = value;
            }
        }

        internal TruckModel()
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
            Properties.Add("Cabin capacity", new List<string>());
            Properties.Add("Dangerous goods", new List<string>() { "1. Yes", "2. No" });
        }

        internal override void UpdateVehicleDetails(string i_Key)
        {
            switch (i_Key)
            {
                case "Cabin capacity":
                    UpdateCabinCapacity(i_Key);
                    break;
                case "Dangerous goods":
                    m_IsTransportingDangerousGoods = Properties["Dangerous goods"].Last() == "1";
                    break;
                default:
                    base.UpdateVehicleDetails(i_Key);
                    break;
            }
        }

        private void UpdateCabinCapacity(string i_Key)
        {
            float cabinCapacity = 0;
            bool isParsedSuccessfully = Single.TryParse(Properties["Cabin capacity"].Last(), out cabinCapacity);

            if (isParsedSuccessfully == false)
            {
                throw new FormatException("Invalid input, value needs to be a float.");
            }

            CabinCapacity = cabinCapacity;
        }

        internal override string GetTypeOfVehicle()
        {
            string typeOfVehicle = "Truck";

            return typeOfVehicle;
        }

        public override string ToString()
        {
            string carDetailsMsg = string.Format("Cabin Capacity: {0}{1}Transporting Dangerous Goods: {2}{1}",
                m_CabinCapacity, Environment.NewLine, m_IsTransportingDangerousGoods ? "Yes" : "No");

            return base.ToString() + carDetailsMsg;
        }
    }
}