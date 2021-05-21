namespace Ex03.GarageLogic
{
    internal struct VehicleOwner
    {
        private readonly string mr_Name;
        private string m_PhoneNumber;

        internal VehicleOwner(string i_Name, string i_PhoneNumber)
        {
            this.mr_Name = i_Name;
            this.m_PhoneNumber = i_PhoneNumber;
        }

        internal string Name
        {
            get
            {
                return this.mr_Name;
            }
        }

        internal string PhoneNumber
        {
            get
            {
                return this.m_PhoneNumber;
            }
            set
            {
                this.m_PhoneNumber = value;
            }
        }

        internal string toString()
        {
            string i_VehicleOwnerDetails = string.Format(@"Owner Name-  {0}
                                                        Owner Phone Number- {1}"
                                        , this.mr_Name, this.m_PhoneNumber);
            return i_VehicleOwnerDetails;
        }
    }
}