using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Models.Energy_Models
{
    internal class ElectricEngineModel : EngineModel
    {
        internal bool RechargeVehicle(float i_AmountOfMinutesToCharge)
        {
            int minimumAmountToRecharge = 0;
            float maxAmountToRecharge = (float)Math.Round(MaxEnergyCapacity - CurrentEnergyAmount, 2);
            bool isRechagedSuccessfull = true;

            if (i_AmountOfMinutesToCharge + CurrentEnergyAmount > MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(minimumAmountToRecharge, maxAmountToRecharge);
            }

            if (EnergyType != eEnergyType.Electric)
            {
                throw new InvalidOperationException("This is a fuel car, this operation cannot be performed");
            }

            CurrentEnergyAmount += i_AmountOfMinutesToCharge;

            return isRechagedSuccessfull;
        }
    }
}