using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Vehicle> m_TreatmentVehiclesInGarage;
        private Dictionary<string, Vehicle> m_FixedVehiclesInGarage;
        private Dictionary<string, Vehicle> m_PayedVehiclesInGarage;

        public Garage()
        {
            this.m_TreatmentVehiclesInGarage = new Dictionary<string, Vehicle>();
            this.m_FixedVehiclesInGarage = new Dictionary<string, Vehicle>();
            this.m_PayedVehiclesInGarage = new Dictionary<string, Vehicle>();
        }

        public void AddVehicle(int i_VehicleType, string i_ModelName, string i_LicensePlateNumber, string i_OwnerName, string i_OwnerPhoneNumber, bool i_IsFuelBased)
        {
            Vehicle i_NewVehicle;
            VehicleOwner i_VehicleOwner;

            if(IsExistInGarage(i_LicensePlateNumber))
            {
                ChangeVehicleStatus(i_LicensePlateNumber, eStatusInGarage.Treatment);
            }
            else
            {
                i_VehicleOwner = new VehicleOwner(i_OwnerName, i_OwnerPhoneNumber);
                i_NewVehicle = CreateVehicle(i_VehicleType, i_ModelName, i_LicensePlateNumber, i_VehicleOwner, i_IsFuelBased);
                AddVehicleToStatusList(i_NewVehicle, eStatusInGarage.Treatment);
            }       
        }

        /*
         * Creates a new vehicle according to the vehicle type
         */
        internal static Vehicle CreateVehicle(int i_VehicleType, string i_ModelName, string i_LicensePlateNumber, VehicleOwner i_Owner, bool i_IsFuelBased)
        {
            Vehicle io_NewVehicle = null;

            switch ((eVehicleTypes)(i_VehicleType))
            {
                case eVehicleTypes.Car:
                    io_NewVehicle = new Car(i_ModelName, i_LicensePlateNumber, i_IsFuelBased, i_Owner);
                    break;
                case eVehicleTypes.Motorcycle:
                    io_NewVehicle = new Motorcycle(i_ModelName, i_LicensePlateNumber, i_IsFuelBased, i_Owner);
                    break;
                case eVehicleTypes.Truck:
                    io_NewVehicle = new Truck(i_ModelName, i_LicensePlateNumber, i_IsFuelBased, i_Owner);
                    break;
            }

            return io_NewVehicle;
        }

        private Vehicle setVehicle(string i_ModelName, string i_LicensePlateNumber, VehicleOwner i_VehicleOwner, bool i_IsFuelBased)
        {
            throw new NotImplementedException();
        }

        public void SetWheels(string i_LicensePlateNumber, string i_ManufacturerName, float i_CurAirPressure)
        {
            Vehicle curVehicle = SearchVehicle(i_LicensePlateNumber);
            curVehicle.SetWheels(i_ManufacturerName, i_CurAirPressure);
        }

        public void SetEnergy(string i_LicensePlateNumber, float i_CurEnergy)
        {
            Vehicle curVehicle = SearchVehicle(i_LicensePlateNumber);
            curVehicle.SetEnergy(i_CurEnergy);
        }

        public bool IsExistInGarage(string i_LicensePlateNumber)
        {
            bool i_IsExist = true;
            if (SearchVehicle(i_LicensePlateNumber) == null)
            {
                i_IsExist = false;
            }
            return i_IsExist;
        }

        private Vehicle SearchVehicle(string i_LicensePlateNumber)
        {
            Vehicle io_FoundedVehicle = null;
            if (this.m_TreatmentVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out io_FoundedVehicle)) ;
            else if (this.m_FixedVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out io_FoundedVehicle)) ;
            else if (this.m_PayedVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out io_FoundedVehicle)) ;
            return io_FoundedVehicle;
        }

        public void ChangeVehicleStatus(string i_LicensePlateNumber, eStatusInGarage i_CurVehicleStatus)
        {
            Vehicle i_vehicleToChange = SearchVehicle(i_LicensePlateNumber);
            if(i_vehicleToChange == null)
            {
                throw new VehicleNotInGarageException();
            }
            RemoveVehicleFromCurStatusList(i_vehicleToChange);
            AddVehicleToStatusList(i_vehicleToChange, eStatusInGarage.Treatment);
        }

        private void RemoveVehicleFromCurStatusList(Vehicle i_VehicleToRemove)
        {
            switch (i_VehicleToRemove.Status)
            {
                case eStatusInGarage.Treatment:
                    this.m_TreatmentVehiclesInGarage.Remove(i_VehicleToRemove.LicensePlateNumber);
                    break;
                case eStatusInGarage.Fixed:
                    this.m_FixedVehiclesInGarage.Remove(i_VehicleToRemove.LicensePlateNumber);
                    break;
                case eStatusInGarage.Payed:
                    this.m_PayedVehiclesInGarage.Remove(i_VehicleToRemove.LicensePlateNumber);
                    break;
            }
        }

        private void AddVehicleToStatusList(Vehicle i_VehicleToAdd, eStatusInGarage i_NewVehicleStatus)
        {
            switch (i_NewVehicleStatus)
            {
                case eStatusInGarage.Treatment:
                    this.m_TreatmentVehiclesInGarage.Add(i_VehicleToAdd.LicensePlateNumber, i_VehicleToAdd);
                    break;
                case eStatusInGarage.Fixed:
                    this.m_FixedVehiclesInGarage.Add(i_VehicleToAdd.LicensePlateNumber, i_VehicleToAdd);
                    break;
                case eStatusInGarage.Payed:
                    this.m_PayedVehiclesInGarage.Add(i_VehicleToAdd.LicensePlateNumber, i_VehicleToAdd);
                    break;
            }
        }

        /*
         * Returns a string with the details of all the vehicles
         * Throws VehicleNotInGarageException when the vehicle isn't in the garage
         */
        private string GetVehicleDetails(string i_LicensePlateNumber)
        {
            Vehicle i_Vehicle = SearchVehicle(i_LicensePlateNumber);
            string io_VehicleDetails;

            if (i_Vehicle == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                io_VehicleDetails = i_Vehicle.ToString();
            }

            return io_VehicleDetails;
        }

        /*
         * Creates a string with all license plates of vehicles in a given state
         */
        private string GetVehiclesOfSameStatus(ref Dictionary<string, Vehicle> io_DictionaryOfVehiclesInStatus)
        {
            StringBuilder i_VehiclesWithSameStatus = new StringBuilder();

            foreach (string i_LicensePlateNumber in io_DictionaryOfVehiclesInStatus.Keys)
            {
                i_VehiclesWithSameStatus.Append(i_LicensePlateNumber + ", ");
            }

            //TODO
            // if (i_VehiclesWithSameStatus.Length > 0)
            //{
              //  i_VehiclesWithSameStatus.Remove(i_VehiclesWithSameStatus.Length - 2, 2);
            //}

            i_VehiclesWithSameStatus.Append(System.Environment.NewLine);

            return i_VehiclesWithSameStatus.ToString();
        }

        /*
          * Returns a string with the license plates in a given status.
          * Receives a boolean array of size 3 representing the states(Treatment, Fixed, Payed).  
          */
        public string GetLicensePlatesByState(ref bool[] io_NecesseryStatuses)
        {
            StringBuilder i_VehiclesByStatus = new StringBuilder();

            for (int i = 0; i < io_NecesseryStatuses.Length; i++)
            {
                if (io_NecesseryStatuses[i] == true)
                {
                    i_VehiclesByStatus.Append(((eStatusInGarage)(i + 1)).ToString() + "- ");
                    switch (i + 1)
                    {
                        case 1:
                            i_VehiclesByStatus.Append(GetVehiclesOfSameStatus(ref this.m_TreatmentVehiclesInGarage));
                            break;
                        case 2:
                            i_VehiclesByStatus.Append(GetVehiclesOfSameStatus(ref this.m_FixedVehiclesInGarage));
                            break;
                        case 3:
                            i_VehiclesByStatus.Append(GetVehiclesOfSameStatus(ref this.m_PayedVehiclesInGarage));
                            break;
                    }
                    i_VehiclesByStatus.Append(System.Environment.NewLine);
                }
            }
            return i_VehiclesByStatus.ToString();
        }

        public int GetNumOfWheelsInVehicle(string i_LicensePlateNumber)
        {
            int numberOfWheels = 0;

            if (IsExistInGarage(i_LicensePlateNumber))
            {
                numberOfWheels = SearchVehicle(i_LicensePlateNumber).NumOfWheels;
            }
            else
            {
                numberOfWheels = 0;
            }

            return numberOfWheels;
        }

        public void FillAirInWheels(string i_LicensePlateNumber, float[] i_AmountOfAirToFill)
        {
            Vehicle curVehicle = SearchVehicle(i_LicensePlateNumber);

            if (curVehicle == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                curVehicle.FillAirInWheels(i_AmountOfAirToFill);
            }
        }

        public bool IsFuelBasedVehicle(string i_LicensePlateNumber)
        {
            Vehicle i_VehiclesByStatus = SearchVehicle(i_LicensePlateNumber);

            if (i_VehiclesByStatus == null)
            {
                throw new VehicleNotInGarageException();
            }

            return i_VehiclesByStatus.IsFuelBased;
        }

        public eStatusInGarage GetStatusByLicensePlateNumber(string i_LicensePlateNumber)
        {
            Vehicle i_Vehicle = SearchVehicle(i_LicensePlateNumber);

            if (i_Vehicle == null)
            {
                throw new VehicleNotInGarageException();
            }

            return i_Vehicle.Status;
        }

        /*
         * Fills the fuel tank of a vehicle.
         * Throws a VehicleNotInGarageException if the vehicle doesn't found
         */
        public void FillEnergy(string i_LicensePlateNumber, float i_EnergyToFill, eFuelTypes i_FuelType)
        {
            Vehicle i_VehicleToFill = SearchVehicle(i_LicensePlateNumber);

            if (i_VehicleToFill == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                i_VehicleToFill.FillEnergy(i_EnergyToFill, i_FuelType);
            }
        }

        /*
         * Fills the battery of a vehicle.
         * Throws a VehicleNotInGarageException if the vehicle doesn't found
         */
        public void FillEnergy(string i_LicensePlateNumber, float i_EnergyToFill)
        {
            Vehicle i_VehicleToFill = SearchVehicle(i_LicensePlateNumber);

            if (i_VehicleToFill == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                i_VehicleToFill.FillEnergy(i_EnergyToFill);
            }
        }

    }

}
