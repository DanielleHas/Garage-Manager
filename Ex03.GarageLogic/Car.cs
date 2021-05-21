using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private const int k_NumOfWheels = 4;
        private const int k_MaxAirPressure = 32;
        private const eFuelTypes k_FuelType = eFuelTypes.Octan95;
        private const float k_MaxCapacityOfFuel = 45f; //in liters
        private const float k_MaxCapacityOfBattery = 3.2f; //in hours
                                                          

        internal Car(string i_ModelName, string i_LicensePlateNumber, bool i_IsFuelBased, VehicleOwner i_VehicleOwner) : base(i_ModelName, i_LicensePlateNumber, i_VehicleOwner, i_IsFuelBased, k_NumOfWheels, k_FuelType, k_MaxAirPressure)
        {
        
        }

        /*
         * Sets a battery for an electric car and fuel tank for a non electric car
         */
        internal override void SetEnergy(float i_CurrentEnergyInEnergySource)
        {
            if (this.IsFuelBased)
            {
                this.m_EnergyType = new Fuel(k_MaxCapacityOfFuel, i_CurrentEnergyInEnergySource, k_FuelType);
            }
            else
            {
                this.m_EnergyType = new Battery(k_MaxCapacityOfBattery, i_CurrentEnergyInEnergySource);
            }
        }
    }
}
