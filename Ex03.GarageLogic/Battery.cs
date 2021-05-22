using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Battery : EnergyType
    {
        internal Battery(float i_MaxCapacity, float i_RemainingFuel) : base(i_MaxCapacity, i_RemainingFuel) { }

        internal override void FillIn(float i_HoursToAdd)
        {
                base.FillIn(i_HoursToAdd);
        }

        internal override string ToString()
        {
            StringBuilder i_BatteryDetails = new StringBuilder();
            i_BatteryDetails.Append("Energy type - Electric" + System.Environment.NewLine);
            i_BatteryDetails.Append(base.ToString());
            return i_BatteryDetails.ToString();
        }
    }
}
