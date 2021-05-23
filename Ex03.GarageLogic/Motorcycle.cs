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
        private eLicensTypes m_LicensType;
        private int m_EnergyCapacity;
        //private const string k_ExtraFearute1 = "Licens type"; //TODO: eFeatures.License_Type.ToString();
        //private const string k_ExtraFearute2 = "Energy capacity"; //TODO: eFeatures.Engine_Capacity.ToString();



        internal Motorcycle(string i_ModelName, string i_LicensePlateNumber, bool i_IsFuelBased, VehicleOwner i_VehicleOwner) : base(i_ModelName, i_LicensePlateNumber, i_VehicleOwner, i_IsFuelBased, k_NumOfWheels, k_FuelType, k_MaxAirPressure)
        {
            //this.mr_ExtraFeaturesList.Add(k_ExtraFearute1);
            //this.mr_ExtraFeaturesList.Add(k_ExtraFearute2);
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

        internal void SetLicensType(String i_LicenseType)
        {
            eLicensTypes io_LicenseType;
            if (!Enum.IsDefined(typeof(eLicensTypes), i_LicenseType))
            {
                if (eLicensTypes.TryParse(i_LicenseType, out io_LicenseType))
                {
                    this.m_LicensType = io_LicenseType;
                }
                else
                {
                    throw new ArgumentException("You can choose only A, B1, AA or BB as a licens type");
                }
            }
        }

        internal void SetEnergyCapacity(String i_EnergyCapacity)
        {
            int io_EnergyCapacity;
            if (!int.TryParse(i_EnergyCapacity, out io_EnergyCapacity))
            {
                if (io_EnergyCapacity < 0)
                {
                    throw new ArgumentException("You can set only a non negative number for Energy Capacity");
                }
                else
                {
                    throw new ArgumentException("You can set up to " + Int32.MaxValue + " for the energy capacity");
                }
            }
            else
            {
                this.m_EnergyCapacity = io_EnergyCapacity;
            }
        }

        internal override string ToString()
        {
            string io_CarDetails = string.Format(@"{0}
                                                   License Type - {1}
                                                   Energy Capacity- {2}"
                                                 , base.ToString(), this.m_LicensType, this.m_EnergyCapacity);
            return io_CarDetails;
        }
    }
}
