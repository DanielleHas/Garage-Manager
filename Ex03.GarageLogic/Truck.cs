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
        private const string k_ExtraFearute1 = "Has dangerous materials"; //TODO: eFeatures.Has_Dangerous_Materials.ToString();
        private const string k_ExtraFearute2 = "Maximum carring weight"; //TODO: eFeatures.MaxCarringWeight.ToString();
        private float m_CarringWeight;
        private bool mv_HasDangerousMaterials;

        internal Truck(string i_ModelName, string i_LicensePlateNumber, bool i_IsFuelBased, VehicleOwner i_VehicleOwner, Dictionary<string, string> i_ExtraFeatursDictionary) : base(i_ModelName, i_LicensePlateNumber, i_VehicleOwner, i_IsFuelBased, k_NumOfWheels, k_FuelType, k_MaxAirPressure)
        {
            this.m_ExtraFeatursDictionary = i_ExtraFeatursDictionary;
            if (i_IsFuelBased)
            {
                this.m_EnergyType = new Fuel(k_MaxCapacityOfFuel, 0, k_FuelType);
            }
        }

        /*
         * Sets a battery for an electric car and fuel tank for a non electric car
         */
        internal override void SetEnergy(float i_CurrentEnergy)
        {
            this.m_EnergyType = new Fuel(k_MaxCapacityOfFuel, i_CurrentEnergy, k_FuelType);
        }

        private void SetCarringWeight(string i_CarringWeight)
        {
            float o_CarringWeight;
            if (float.TryParse(i_CarringWeight, out o_CarringWeight))
            {
                this.m_CarringWeight = o_CarringWeight;
            }
            else if (o_CarringWeight < 0)
            {
                throw new ArgumentException("You can set only a non negative number for the carring weight");
            }
        }

        private void SetDangerousMaterials(string i_HasDangerousMaterials)
        {
            if (i_HasDangerousMaterials.Equals('Y'))
            {
                this.mv_HasDangerousMaterials = true;

            }
            else
            {
                this.mv_HasDangerousMaterials = false;

            } 
        }

        /*
         * Sets the extra features that truck has. 
         */
        internal override void SetExtraFeatures(Dictionary<string, string> i_ExtraFeatures)
        {
            string o_CarringWeight;
            string o_HasDangerousMaterials;

            i_ExtraFeatures.TryGetValue(k_ExtraFearute1, out o_CarringWeight);
            i_ExtraFeatures.TryGetValue(k_ExtraFearute2, out o_HasDangerousMaterials);
            SetCarringWeight(o_CarringWeight) ;
            SetDangerousMaterials(o_HasDangerousMaterials);
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
