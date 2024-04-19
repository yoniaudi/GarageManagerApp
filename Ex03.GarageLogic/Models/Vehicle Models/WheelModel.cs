using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Models.Vehicle_Models
{
    internal class WheelModel
    {
        internal string Manufecturer { get; set; } = null;
        internal float MaxAirPressure { get { return r_MaxAirPressure; } }
        private readonly float r_MaxAirPressure = 0f;
        private float m_CurrentAirPressure = 0f;

        internal float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set
            {
                if (value < 0 || value > r_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure);
                }

                m_CurrentAirPressure = value;
            }
        }

        internal WheelModel(float i_MaxAirPressure)
        {
            if (i_MaxAirPressure < 0)
            {
                throw new ArgumentException("Error! Air Pressure Must be greater than 0.");
            }

            r_MaxAirPressure = i_MaxAirPressure;
        }

        internal void InflateVehicleWheels(float i_AirPressureAmount)
        {
            if (CurrentAirPressure < 0 || m_CurrentAirPressure + i_AirPressureAmount > r_MaxAirPressure)
            {
                float minAirPressure = 0f;

                throw new ValueOutOfRangeException(minAirPressure, r_MaxAirPressure);
            }

            m_CurrentAirPressure += i_AirPressureAmount;
        }

        public override string ToString()
        {
            string wheelsMsg = string.Format("  Manufecturer: {0}{1}  Pressure: {2}/{3}", Manufecturer, Environment.NewLine,
                CurrentAirPressure, MaxAirPressure);

            return wheelsMsg;
        }
    }
}
