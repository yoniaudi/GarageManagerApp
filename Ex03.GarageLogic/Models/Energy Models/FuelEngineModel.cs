using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Models.Energy_Models
{
    internal class FuelEngineModel : EngineModel
    {
        internal bool Refuel(eEnergyType i_FuelType, float i_AmountOfFuelToFill)
        {
            int minimumAmountToRefuel = 0;
            float maxAmountToRefuel = (float)Math.Round(MaxEnergyCapacity - CurrentEnergyAmount, 2);
            bool isRefuelSuccessfull = true;

            if (EnergyType != i_FuelType)
            {
                throw new ArgumentException("Error! The fuel type doesn't match the vehicle engine.");
            }
            else if (i_AmountOfFuelToFill + CurrentEnergyAmount > MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(minimumAmountToRefuel, maxAmountToRefuel);
            }

            CurrentEnergyAmount += i_AmountOfFuelToFill;

            return isRefuelSuccessfull;
        }
    }
}