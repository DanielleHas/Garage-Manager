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
        private const string k_ExtraFearute1 = "Color"; //TODO: eFeatures.Color.ToString();
        private const string k_ExtraFearute2 = "Numbe of doors"; //TODO: eFeatures.Num_Of_Doors.ToString();


        internal Car(string i_ModelName, string i_LicensePlateNumber, bool i_IsFuelBased, VehicleOwner i_VehicleOwner) : base(i_ModelName, i_LicensePlateNumber, i_VehicleOwner, i_IsFuelBased, k_NumOfWheels, k_FuelType, k_MaxAirPressure)
        {
           this.mr_ExtraFeaturesList.Add(k_ExtraFearute1);
           this.mr_ExtraFeaturesList.Add(k_ExtraFearute2);
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
            eColors io_Color;
            if (!Enum.IsDefined(typeof(eColors), i_Color))
            {
                if (eColors.TryParse(i_Color, out io_Color))
                {
                    this.m_Color = io_Color;
                }
                else
                { 
                    throw new ArgumentException("You can choose only Red, Silver, White or Black color to the car");
                }
            }
        }

        internal void SetNumOfDoors(String i_NumOfDoors)
        {
            eNumsOfDoors io_NumOfDoors;
            if (!Enum.IsDefined(typeof(eNumsOfDoors), i_NumOfDoors))
            {
                if (eColors.TryParse(i_NumOfDoors, out io_NumOfDoors))
                {
                    this.m_NumOfDoors = io_NumOfDoors;
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
            string io_CarDetails = string.Format(@"{0}
                                                   Color- {1}
                                                   Number Of Doors- {2}"
                                                 , base.ToString(), this.m_Color, this.m_NumOfDoors);
            return io_CarDetails;
        }
    }
}
