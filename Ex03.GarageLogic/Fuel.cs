using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Fuel : EnergyType
    {
        private readonly eFuelTypes mr_FuelType;

        internal Fuel(float i_MaxCapacity, float i_RemainingFuel, eFuelTypes i_FuelType) : base(i_MaxCapacity, i_RemainingFuel)
        {
            this.mr_FuelType = i_FuelType;
        }

        internal void FillIn(float i_FuelToAdd, eFuelTypes i_FuelType)
        {
            if (i_FuelType.Equals(mr_FuelType))
            {
                base.FillIn(i_FuelToAdd);
            }
            else
            {
                throw new ArgumentException(i_FuelType.ToString());
            }
        }

        internal override string ToString()
        {
            StringBuilder i_FuelDetails = new StringBuilder();
            i_FuelDetails.Append("Energy type - Fuel." + System.Environment.NewLine);
            i_FuelDetails.Append("Fuel type - " + mr_FuelType + System.Environment.NewLine);
            i_FuelDetails.Append(base.ToString());
            return i_FuelDetails.ToString();
        }
    }
}
