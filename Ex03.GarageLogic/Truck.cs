using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private const int k_NumOfWheels = 16;
        private const int k_MaxAirPressure = 26;
        private const eFuelTypes k_FuelType = eFuelTypes.Soler;
        private const float k_MaxCapacityOfFuel = 120f; //in liters
        //private const string k_ExtraFearute1 = "Has dangerous materials"; //TODO: eFeatures.Has_Dangerous_Materials.ToString();
        //private const string k_ExtraFearute2 = "Maximum carring wight"; //TODO: eFeatures.MaxCarringWeight.ToString();
        private float m_CarringWeight;


        internal Truck(string i_ModelName, string i_LicensePlateNumber, bool i_IsFuelBased, VehicleOwner i_VehicleOwner) : base(i_ModelName, i_LicensePlateNumber, i_VehicleOwner, i_IsFuelBased, k_NumOfWheels, k_FuelType, k_MaxAirPressure)
        {
            //this.mr_ExtraFeaturesList.Add(k_ExtraFearute1);
            //this.mr_ExtraFeaturesList.Add(k_ExtraFearute2);
        }

        /*
         * Sets a battery for an electric car and fuel tank for a non electric car
         */
        internal override void SetEnergy(float i_CurrentEnergy)
        {
            this.m_EnergyType = new Fuel(k_MaxCapacityOfFuel, i_CurrentEnergy, k_FuelType);
        }

        private void setCarringWeight(string i_CarringWeight)
        {
            float io_CarringWeight;
            if (float.TryParse(i_CarringWeight, out io_CarringWeight))
            {
                this.m_CarringWeight = io_CarringWeight;
            }
            else if (io_CarringWeight < 0)
            {
                throw new ArgumentException("You can set only a non negative number for the carring weight");
            }
        }

        internal override string ToString()
        {
            string io_TruckDetails = string.Format(@"{0}
                                                        Current Carring Weight- {1}"
                                                        , base.ToString(), this.m_CarringWeight);
            return io_TruckDetails;
        }
    }
}
