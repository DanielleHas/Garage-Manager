using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
        private const int k_NumOfWheels = 2;
        private const int k_MaxAirPressure = 30;
        private const eFuelTypes k_FuelType = eFuelTypes.Octan98;
        private const float k_MaxCapacityOfFuel = 6f; //in liters
        private const float k_MaxCapacityOfBattery = 1.8f; //in hours      
        private eLicensTypes m_LicenseType;
        private int m_EngineCapacity;



        internal Motorcycle(string i_ModelName, string i_LicensePlateNumber, bool i_IsFuelBased, VehicleOwner i_VehicleOwner, eLicensTypes i_LicensType, int i_EngineCapacity) : base(i_ModelName, i_LicensePlateNumber, i_VehicleOwner, i_IsFuelBased, k_NumOfWheels, k_FuelType, k_MaxAirPressure)
        {
            this.m_LicenseType = i_LicensType;
            this.m_EngineCapacity = i_EngineCapacity;
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
