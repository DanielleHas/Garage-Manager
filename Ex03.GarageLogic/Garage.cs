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

        public void AddVehicle(int i_VehicleType, string i_ModelName, string i_LicensePlateNumber, string i_OwnerName, string i_OwnerPhoneNumber, bool i_IsFuelBased, Dictionary<string, string> i_ExtraFeatursDictionary)
        {
            Vehicle newVehicle;
            VehicleOwner vehicleOwner;

            if (IsExistInGarage(i_LicensePlateNumber))
            {
                ChangeVehicleStatus(i_LicensePlateNumber, eStatusInGarage.Treatment);
            }
            else
            {
                vehicleOwner = new VehicleOwner(i_OwnerName, i_OwnerPhoneNumber);
                newVehicle = CreateVehicle(i_VehicleType, i_ModelName, i_LicensePlateNumber, vehicleOwner, i_IsFuelBased, i_ExtraFeatursDictionary);
                AddVehicleToStatusList(newVehicle, eStatusInGarage.Treatment);
                newVehicle.Status = eStatusInGarage.Treatment;
            }
        }

        /*
         * Creates a new vehicle according to the vehicle type
         */
        internal static Vehicle CreateVehicle(int i_VehicleType, string i_ModelName, string i_LicensePlateNumber, VehicleOwner i_Owner, bool i_IsFuelBased, Dictionary<string, string> i_ExtraFeatursDictionary)
        {
            Vehicle o_NewVehicle = null;

            switch ((eVehicleTypes)(i_VehicleType))
            {
                case eVehicleTypes.Car:
                    o_NewVehicle = new Car(i_ModelName, i_LicensePlateNumber, i_IsFuelBased, i_Owner, i_ExtraFeatursDictionary);
                    break;
                case eVehicleTypes.Motorcycle:
                    o_NewVehicle = new Motorcycle(i_ModelName, i_LicensePlateNumber, i_IsFuelBased, i_Owner, i_ExtraFeatursDictionary);
                    break;
                case eVehicleTypes.Truck:
                    o_NewVehicle = new Truck(i_ModelName, i_LicensePlateNumber, i_IsFuelBased, i_Owner, i_ExtraFeatursDictionary);
                    break;
            }

            return o_NewVehicle;
        }

        private Vehicle SetVehicle(string i_ModelName, string i_LicensePlateNumber, VehicleOwner vehicleOwner, bool i_IsFuelBased)
        {
            throw new NotImplementedException();
        }

        public void SetWheels(string i_LicensePlateNumber, string i_ManufacturerName, float i_CurAirPressure)
        {
            Vehicle curVehicle = SearchVehicle(i_LicensePlateNumber);
            if (curVehicle == null)
            {
                return;
            }
            curVehicle.SetWheels(i_ManufacturerName, i_CurAirPressure);
        }

        public void SetEnergy(string i_LicensePlateNumber, float i_CurEnergy)
        {
            Vehicle curVehicle = SearchVehicle(i_LicensePlateNumber);
            if (curVehicle == null)
            {
                return;
            }
            curVehicle.SetEnergy(i_CurEnergy);
        }

        /*
         * Sets extra features to the vehicle
         */
        public void SetExtraFeaturesToVehicle(string i_LicensePlateNumber, ref Dictionary<string, string> io_ExtraFeaturesToSet)
        {
            Vehicle curVehicle = SearchVehicle(i_LicensePlateNumber);
            curVehicle.SetExtraFeatures(io_ExtraFeaturesToSet);
        }

        public bool IsExistInGarage(string i_LicensePlateNumber)
        {
            bool v_IsExist = true;
            if (SearchVehicle(i_LicensePlateNumber) == null)
            {
                v_IsExist = false;
            }
            return v_IsExist;
        }

        private Vehicle SearchVehicle(string i_LicensePlateNumber)
        {
            Vehicle o_FoundedVehicle = null;
            if (this.m_TreatmentVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out o_FoundedVehicle)) ;
            else if (this.m_FixedVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out o_FoundedVehicle)) ;
            else if (this.m_PayedVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out o_FoundedVehicle)) ;
            return o_FoundedVehicle;
        }

        public void ChangeVehicleStatus(string i_LicensePlateNumber, eStatusInGarage i_DesiredVehicleStatus)
        {
            Vehicle vehicleToChange = SearchVehicle(i_LicensePlateNumber);
            if(vehicleToChange == null)
            {
                throw new VehicleNotInGarageException();
            }
            RemoveVehicleFromCurStatusList(vehicleToChange);
            AddVehicleToStatusList(vehicleToChange, i_DesiredVehicleStatus);
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

        private void AddVehicleToStatusList(Vehicle i_VehicleToAdd, eStatusInGarage newVehicleStatus)
        {
            switch (newVehicleStatus)
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
        public string GetVehicleDetails(string i_LicensePlateNumber)
        {
            Vehicle vehicle = SearchVehicle(i_LicensePlateNumber);
            string o_VehicleDetails;

            if (vehicle == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                o_VehicleDetails = vehicle.ToString();
            }

            return o_VehicleDetails;
        }

        /*
         * Creates a string with all license plates of vehicles in a given state
         */
        private string GetVehiclesOfSameStatus(ref Dictionary<string, Vehicle> io_DictionaryOfVehiclesInStatus)
        {
            StringBuilder vehiclesWithSameStatus = new StringBuilder();

            foreach (string i_LicensePlateNumber in io_DictionaryOfVehiclesInStatus.Keys)
            {
                vehiclesWithSameStatus.Append(i_LicensePlateNumber + ", ");
            }

            vehiclesWithSameStatus.Append(System.Environment.NewLine);

            return vehiclesWithSameStatus.ToString();
        }

        /*
         * Receives a boolean array of size 3 representing the states(Treatment, Fixed, Payed).  
         * Returns a string with the license plates in a given status.
         */
        public string GetLicensePlatesByState(ref bool[] io_NecesseryStatuses)
        {
            StringBuilder vehiclesByStatus = new StringBuilder();

            for (int i = 0; i < io_NecesseryStatuses.Length; i++)
            {
                if (io_NecesseryStatuses[i] == true)
                {
                    vehiclesByStatus.Append(((eStatusInGarage)(i + 1)).ToString() + "- ");
                    switch (i + 1)
                    {
                        case 1:
                            vehiclesByStatus.Append(GetVehiclesOfSameStatus(ref this.m_TreatmentVehiclesInGarage));
                            break;
                        case 2:
                            vehiclesByStatus.Append(GetVehiclesOfSameStatus(ref this.m_FixedVehiclesInGarage));
                            break;
                        case 3:
                            vehiclesByStatus.Append(GetVehiclesOfSameStatus(ref this.m_PayedVehiclesInGarage));
                            break;
                    }
                    vehiclesByStatus.Append(System.Environment.NewLine);
                }
            }
            return vehiclesByStatus.ToString();
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

        public Dictionary<string, string> GetExtraFeatures(string i_LicensePlateNumber)
        {
            Vehicle curVehicle = SearchVehicle(i_LicensePlateNumber);
            Dictionary<string, string> extraFeatures = null;
            if (curVehicle == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                extraFeatures = curVehicle.GetExtraFeatursDictionary();
            }
            return extraFeatures;
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
            Vehicle vehiclesByStatus = SearchVehicle(i_LicensePlateNumber);

            if (vehiclesByStatus == null)
            {
                return false;
            }

            return vehiclesByStatus.IsFuelBased;
        }

        public eStatusInGarage GetStatusByLicensePlateNumber(string i_LicensePlateNumber)
        {
            Vehicle vehicle = SearchVehicle(i_LicensePlateNumber);

            if (vehicle == null)
            {
                throw new VehicleNotInGarageException();
            }

            return vehicle.Status;
        }

        /*
         * Fills the fuel tank of a vehicle.
         * Throws a VehicleNotInGarageException if the vehicle doesn't found
         */
        public void FillEnergy(string i_LicensePlateNumber, float i_EnergyToFill, eFuelTypes i_FuelType)
        {
            Vehicle vehicleToFill = SearchVehicle(i_LicensePlateNumber);

            if (vehicleToFill == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                vehicleToFill.FillEnergy(i_EnergyToFill, i_FuelType);
            }
        }

        /*
         * Fills the battery of a vehicle.
         * Throws a VehicleNotInGarageException if the vehicle doesn't found
         */
        public void FillEnergy(string i_LicensePlateNumber, float i_EnergyToFill)
        {
            Vehicle vehicleToFill = SearchVehicle(i_LicensePlateNumber);

            if (vehicleToFill == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                vehicleToFill.FillEnergy(i_EnergyToFill);
            }
        }

    }

}
