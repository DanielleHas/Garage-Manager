using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    abstract class EnergyType
    {
        private protected readonly float mr_MaxCapacity;
        private protected float  m_CurrentAmount;
        private protected float m_FullPrecentage;

        internal EnergyType(float i_MaxCapacity, float i_RemainingEnergy)
        {
            if (i_RemainingEnergy > i_MaxCapacity || i_RemainingEnergy < 0)
            {
                throw new ValueOutOfRangeException(i_MaxCapacity, 0);
            }
            else
            {
                this.mr_MaxCapacity = i_MaxCapacity;
                this.m_CurrentAmount = i_RemainingEnergy;
                this.m_FullPrecentage = this.m_CurrentAmount / this.mr_MaxCapacity * 100;
            }
        }

        internal virtual void FillIn(float i_EnergyAmuontToFill)
        {

            if (i_EnergyAmuontToFill < 0 || i_EnergyAmuontToFill + this.m_CurrentAmount > this.mr_MaxCapacity)
                {
                    throw new ValueOutOfRangeException(0, this.mr_MaxCapacity);
                }
                else
                {
                    this.m_CurrentAmount += i_EnergyAmuontToFill;
                    this.m_FullPrecentage = this.m_CurrentAmount / this.mr_MaxCapacity * 100;
                }
        }

        internal virtual string ToString()
        {
            string i_EnergyDetails = string.Format(@"Max capacity-  {0}
                                                        Current amount- {1}
                                                        Precentage- {2}"
                                                        , this.mr_MaxCapacity, this.m_CurrentAmount , this.m_FullPrecentage);
            return i_EnergyDetails;
        }
    }
}
