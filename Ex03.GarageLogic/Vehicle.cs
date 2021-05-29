using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    abstract class Vehicle
    {
        internal class Wheel
        {
            private readonly string mr_ManufacturerName;
            private float m_CurAirPressure;
            private float m_MaxAirPressure;

            internal Wheel(string i_ManufacturerName, float i_CurAirPressure, float i_MaxAirPressure)
            {
                if (i_CurAirPressure > i_MaxAirPressure || i_CurAirPressure < 0)
                {
                    throw new ValueOutOfRangeException(i_MaxAirPressure, 0);
                }
                else
                {
                    this.mr_ManufacturerName = i_ManufacturerName;
                    this.m_CurAirPressure = i_CurAirPressure;
                    this.m_MaxAirPressure = i_MaxAirPressure;
                }
            }

            /*
             * Inflate the wheel method
             * input = -1 to fill the wheel to its max air pressure
             * Throws ValueOutOfRangeException when the input is out of range
            */
            private void inflate(float i_AirToAdd)
            {
                if (i_AirToAdd == -1)
                {
                    m_CurAirPressure = m_MaxAirPressure;
                }
                else if (i_AirToAdd < 0 || i_AirToAdd + m_CurAirPressure > m_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, m_MaxAirPressure);
                }
                else
                {
                    m_CurAirPressure += i_AirToAdd;
                }
            }

            private string ToString()
            {
                string io_WheelDetails = string.Format(@"Manufacturer Name-  {0}
                                                        Maximum Air Pressure- {1}
                                                        Current Air Pressure- {2}"
                                                        , this.mr_ManufacturerName, this.m_MaxAirPressure, this.m_CurAirPressure);
                return io_WheelDetails;
            }
        }

        protected readonly string mr_ModelName;
        protected readonly string mr_LicensePlateNumber;
        protected float m_RemainingEnergyPrecent;
        protected readonly bool mvr_IsFuelBased;
        protected Wheel[] m_Wheels;
        protected float m_MaxAirPressure;
        protected VehicleOwner m_VehicleOwner;
        protected EnergyType m_EnergyType;
        protected readonly eFuelTypes mr_FuelType;
        protected eStatusInGarage m_CurStatus;
        //protected readonly List<string> mr_ExtraFeaturesList;

        internal Vehicle(string i_ModelName, string i_LicensePlateNumber, VehicleOwner i_VehicleOwner, bool i_IsFuelBased, int i_NumOfWheels, eFuelTypes i_FuelType, float i_MaxAirPressure)
        {
            this.mr_ModelName = i_ModelName;
            this.mr_LicensePlateNumber = i_LicensePlateNumber;
          //  this.m_RemainingEnergyPrecent = i_RemainingEnergyPrecent;
            this.m_Wheels = new Wheel[i_NumOfWheels];
            this.m_MaxAirPressure = i_MaxAirPressure;
            this.mvr_IsFuelBased = i_IsFuelBased;
            this.m_VehicleOwner = i_VehicleOwner;
           // this.mr_ExtraFeaturesList = new List<string>();

            if (this.mvr_IsFuelBased)
            {
                this.mr_FuelType = i_FuelType;
            } 
        }

        internal eStatusInGarage Status
        {
            get
            {
                return this.m_CurStatus;
            }
            set
            {
                this.m_CurStatus = value;
            }
        }
        
        internal bool IsFuelBased
        {
            get
            {
                return this.mvr_IsFuelBased;
            }
        }

        internal string LicensePlateNumber
        {
            get
            {
                return this.mr_LicensePlateNumber;
            }
        }

        internal Wheel[] Wheels
        {
            get
            {
                return this.m_Wheels;
            }
            set
            {
                this.m_Wheels = value;
            }
        }

        internal int NumOfWheels
        {
            get
            {
                return this.m_Wheels.Length;
            }
        }

        /*
         * Set each wheel of the vehicle
        */
        internal void SetWheels(string i_ManufacturerName, float i_CurAirPressure)
        {
            for(int i = 0; i < this.m_Wheels.Length; i++)
            {
                this.m_Wheels[i] = new Wheel(i_ManufacturerName, i_CurAirPressure, this.m_MaxAirPressure);
            }
        }

        internal abstract void SetEnergy(float i_CurEnergy);
        // internal abstract void SetExtraFeatures(Dictionary<string, string> i_ExtraFeaturesDictionary);


        /*
         * Charges fueld-based vehicles 
         */
        internal void FillEnergy(float i_EnergyToFill, eFuelTypes i_FuelType)
        {
            (this.m_EnergyType as Fuel).FillIn(i_EnergyToFill, i_FuelType);
        }

        /*
         * Charges electric vehicles
         */
        internal void FillEnergy(float i_EnergyToFill)
        {
            (this.m_EnergyType as Battery).FillIn(i_EnergyToFill);
        }

        /*
        * Returns a string with the wheels details
        */
        private string WheelsDetails()
        {
            StringBuilder i_WheelsDetails = new StringBuilder();
            int i_WheelIndex = 1;

            foreach (Wheel wheel in this.m_Wheels)
            {
                i_WheelsDetails.Append("Wheel number " + i_WheelIndex + "- " + System.Environment.NewLine);
                i_WheelsDetails.Append(wheel.ToString());
                i_WheelIndex++;
            }

            return i_WheelsDetails.ToString();
        }

        internal virtual string ToString()
        {
            StringBuilder i_VehicleDetails = new StringBuilder();

            i_VehicleDetails.Append("License plate number- " + this.mr_LicensePlateNumber + System.Environment.NewLine);
            i_VehicleDetails.Append("Vehicle model- " + this.mr_ModelName + System.Environment.NewLine);
            i_VehicleDetails.Append(this.m_VehicleOwner.toString() + System.Environment.NewLine);
            i_VehicleDetails.Append("Wheels- " + System.Environment.NewLine);
            i_VehicleDetails.Append(WheelsDetails());
            i_VehicleDetails.Append(this.m_EnergyType.ToString());

            return i_VehicleDetails.ToString();
        }

        
    }
}
