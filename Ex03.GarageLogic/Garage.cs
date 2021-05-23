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

        private bool IsExistInGarage(string i_LicensePlateNumber)
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
            Vehicle io_FoundedVehicle;
            if (this.m_TreatmentVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out io_FoundedVehicle)) ;
            else if (this.m_FixedVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out io_FoundedVehicle)) ;
            else if (this.m_PayedVehiclesInGarage.TryGetValue(i_LicensePlateNumber, out io_FoundedVehicle)) ;
            return io_FoundedVehicle;
        }

        private void ChangeVehicleStatus(string i_LicensePlateNumber, eStatusInGarage i_CurVehicleStatus)
        {
            Vehicle i_vehicleToChange = SearchVehicle(i_LicensePlateNumber);
            if(i_vehicleToChange == null)
            {
                // TODO: throw 
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
    }

    }
