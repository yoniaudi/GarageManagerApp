using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Models.Energy_Models
{
    internal abstract class EngineModel
    {
        internal eEnergyType EnergyType { get; set; } = new eEnergyType();
        internal float MaxEnergyCapacity { get; private set; } = 0f;
        private float m_CurrentEnergyAmount = 0f;

        internal float CurrentEnergyAmount
        {
            get { return m_CurrentEnergyAmount; }
            set
            {
                if (value > MaxEnergyCapacity)
                {
                    throw new ValueOutOfRangeException(0, MaxEnergyCapacity);
                }

                m_CurrentEnergyAmount = value;
            }
        }

        internal void SetEngineType(eEnergyType i_EnergyType, float i_EnergyMaxCapacity, float i_CurrentEnergyAmount)
        {
            int minimumAmountToRefuel = 0;

            switch (i_EnergyType)
            {
                case eEnergyType.Soler:
                    EnergyType = eEnergyType.Soler;
                    break;
                case eEnergyType.Octan95:
                    EnergyType = eEnergyType.Octan95;
                    break;
                case eEnergyType.Octan96:
                    EnergyType = eEnergyType.Octan96;
                    break;
                case eEnergyType.Octan98:
                    EnergyType = eEnergyType.Octan98;
                    break;
                case eEnergyType.Electric:
                    EnergyType = eEnergyType.Electric;
                    break;
                default:
                    throw new ArgumentException("Error! Gas type is not valid.");
            }

            if (i_EnergyMaxCapacity < 0)
            {
                throw new ValueOutOfRangeException("The energy capacity should be a positive number.");
            }
            else if (i_CurrentEnergyAmount < 0 || i_CurrentEnergyAmount > i_EnergyMaxCapacity)
            {
                throw new ValueOutOfRangeException(minimumAmountToRefuel, i_EnergyMaxCapacity);
            }

            MaxEnergyCapacity = i_EnergyMaxCapacity;
            CurrentEnergyAmount = i_CurrentEnergyAmount;
        }

        public override string ToString()
        {
            float energyPercantage = (float)Math.Round((CurrentEnergyAmount / MaxEnergyCapacity) * 100, 2);
            string engineMsg = string.Format("Engine Type: {0}{1}Energy Amount: {2}%", EnergyType, Environment.NewLine, energyPercantage);

            return engineMsg;
        }
    }
}