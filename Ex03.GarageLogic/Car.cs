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
        private eColors m_Color;
        private eNumsOfDoors m_NumOfDoors;
        private const string k_ExtraFearute1 = "Color";
        private const string k_ExtraFearute2 = "Number of doors"; 


        internal Car(string i_ModelName, string i_LicensePlateNumber, bool i_IsFuelBased, VehicleOwner i_VehicleOwner, Dictionary<string, string> i_ExtraFeatursDictionary) : base(i_ModelName, i_LicensePlateNumber, i_VehicleOwner, i_IsFuelBased, k_NumOfWheels, k_FuelType, k_MaxAirPressure)
        {
            this.m_ExtraFeatursDictionary = i_ExtraFeatursDictionary; 
            string o_Feature1Value;
            string o_Feature2Value;
            bool v_IsSucceed = this.m_ExtraFeatursDictionary.TryGetValue(k_ExtraFearute1, out o_Feature1Value);
            if (v_IsSucceed)
            {
                SetColor(o_Feature1Value);
            }
            v_IsSucceed = this.m_ExtraFeatursDictionary.TryGetValue(k_ExtraFearute2, out o_Feature2Value);
            if (v_IsSucceed)
            {
                SetNumOfDoors(o_Feature2Value);
            }
            if (i_IsFuelBased)
           {
               this.m_EnergyType = new Fuel(k_MaxCapacityOfFuel, 0, k_FuelType);
           }
           else
           {
               this.m_EnergyType = new Battery(k_MaxCapacityOfBattery, 0);
           }
        }

        /*
         * Sets a battery for an electric car and fuel tank for a non electric car
         */
        internal override void SetEnergy(float i_CurrentEnergy)
        {
            if (this.IsFuelBased)
            {
                this.m_EnergyType = new Fuel(k_MaxCapacityOfFuel, i_CurrentEnergy, k_FuelType);
            }
            else
            {
                this.m_EnergyType = new Battery(k_MaxCapacityOfBattery, i_CurrentEnergy);
            }
        }

        internal void SetColor(String i_Color)
        {
            eColors o_Color;
            if (Enum.IsDefined(typeof(eColors), i_Color))
            {
                if (eColors.TryParse(i_Color, out o_Color))
                {
                    this.m_Color = o_Color;
                }
                else
                { 
                    throw new ArgumentException("You can choose only Red, Silver, White or Black color to the car");
                }
            }
        }

        internal void SetNumOfDoors(String i_NumOfDoors)
        {
            eNumsOfDoors o_NumOfDoors;
            if (Enum.IsDefined(typeof(eNumsOfDoors), i_NumOfDoors))
            {
                if (eColors.TryParse(i_NumOfDoors, out o_NumOfDoors))
                {
                    this.m_NumOfDoors = o_NumOfDoors;
                }
                else
                { 
                    throw new ArgumentException("You can choose only Two, Three, Four or Five doors to the car");
                }
            }
        }

        /*
         * Sets the extra features that car has. 
         */
        internal override void SetExtraFeatures(Dictionary<string, string> i_ExtraFeatures)
        {
            string o_Color;
            string o_NumOfDoors;

            i_ExtraFeatures.TryGetValue(k_ExtraFearute1, out o_Color);
            i_ExtraFeatures.TryGetValue(k_ExtraFearute2, out o_NumOfDoors);
            SetColor(o_Color);
            SetNumOfDoors(o_NumOfDoors);
        }

        internal override string ToString()
        {
            string carDetails = string.Format(@"
                                {0}
                                Color- {1}
                                Number Of Doors- {2}"
                                                 , base.ToString(), this.m_Color.ToString(), this.m_NumOfDoors.ToString());
            return carDetails;
        }
    }
}
