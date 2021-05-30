using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class GarageManager
    {
        private Garage m_MyGarage;
        private readonly float r_FillAirToMax = -1.0F;

        internal GarageManager()
        {
            this.m_MyGarage = new Garage();
        }

        internal void AddNewVehicle()
        {
            int o_VehicleType = 0;
            string o_ModelName;
            string o_LicensePlateNumber;
            string o_OwnerName;
            string o_OwnerPhoneNumber;
            char o_IsFuelBased;

            ChatBot.GetVehicleGeneralDetails(out o_VehicleType, out o_ModelName, out o_IsFuelBased, out o_LicensePlateNumber);
            SetOwnerDetails(out o_OwnerName, out o_OwnerPhoneNumber);
            bool v_IsFuelBased = o_IsFuelBased == 'F' || o_IsFuelBased == 'f' ? true : false;
            Dictionary<string, string> extraFeatursDictionary = GetExtraFeatures(o_VehicleType);
            m_MyGarage.AddVehicle(o_VehicleType, o_ModelName, o_LicensePlateNumber, o_OwnerName, o_OwnerPhoneNumber, v_IsFuelBased, extraFeatursDictionary);
            SetEnergyType(o_LicensePlateNumber);
            SetWheels(o_LicensePlateNumber);
        }

        private string GetLicensePlateNumber()
        {
            string licensePlateNumber = ChatBot.GetLicensePlateNumber();

            while (!this.m_MyGarage.IsExistInGarage(licensePlateNumber))
            {
                try
                {
                    licensePlateNumber = ChatBot.GetLicensePlateNumber();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return licensePlateNumber;
        }

        internal void FillEnergyInVehicle(bool isFuelRequired)
        {
            bool chargedSuccessfully = false;
            bool isFuelVehicle;
            float amountToFill = 0;
            eFuelTypes fuelType;
            string fuelTypeCode;
            string licensePlateNumber = GetLicensePlateNumber();

            while (!chargedSuccessfully)
            {
                try
                {
                    isFuelVehicle = this.m_MyGarage.IsFuelBasedVehicle(licensePlateNumber);
                    if (isFuelVehicle)
                    {
                        if (isFuelRequired)
                        {
                            ChatBot.GetFuelingDetails(licensePlateNumber, out amountToFill, out fuelTypeCode);
                            eFuelTypes.TryParse(fuelTypeCode, out fuelType);
                            this.m_MyGarage.FillEnergy(licensePlateNumber, amountToFill, fuelType);
                        }
                        else
                        {
                            Console.WriteLine("Your vehicle based on Fuel, please select option #5.");
                        }
                    }
                    else
                    {
                        if (!isFuelRequired)
                        {
                            amountToFill = ChatBot.GetChargingDetails(licensePlateNumber);
                            this.m_MyGarage.FillEnergy(licensePlateNumber, amountToFill);
                        }
                        else
                        {
                            Console.WriteLine("Your vehicle based on Electric, please select option #6.");
                        }
                    }
                    Console.WriteLine("Please prees enter");
                    Console.ReadLine();
                    chargedSuccessfully = true;
                }
                catch (VehicleNotInGarageException e)
                {
                    ChatBot.PrintLicensePlateNotFoundMessage(licensePlateNumber);
                }
                catch (ValueOutOfRangeException e)
                {
                    ChatBot.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
                catch (System.ArgumentException e)
                {
                    eFuelTypes.TryParse(e.Message, out fuelType);
                    ChatBot.PrintFuelTypeErrorMessage(fuelType);
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        internal void CheckVehicleStatus()
        {
            string licensePlateNumber = GetLicensePlateNumber();

            try
            {
                eStatusInGarage vehicleState = this.m_MyGarage.GetStatusByLicensePlateNumber(licensePlateNumber);
                ChatBot.PrintStatus(vehicleState, licensePlateNumber);
            }
            catch (VehicleNotInGarageException e)
            {
                ChatBot.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        private Dictionary<string, string> GetExtraFeatures(int io_VehicleType)
        {
            Dictionary<string, string> extraFeatursDictionary = ChatBot.GetExtraFeatures(io_VehicleType);
            return extraFeatursDictionary;
        }

        private void SetOwnerDetails(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            bool isSuccessPhoneNumber = false;
            o_OwnerPhoneNumber = "";
            ChatBot.GetOwnerName(out o_OwnerName);
            while (!isSuccessPhoneNumber)
            {
                try
                {
                    ChatBot.GetOwnerPhoneNumber(o_OwnerName, out o_OwnerPhoneNumber);
                    isSuccessPhoneNumber = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void SetWheels(string i_LicensePlateNumber)
        {
            string manufacturerName;
            float curAirPressure = 0;
            bool isSuccess = false;
            ChatBot.GetWheelsManufacturer(i_LicensePlateNumber, out manufacturerName);
            while (!isSuccess)
            {
                try
                {
                    curAirPressure = ChatBot.GetCurAirPressure(i_LicensePlateNumber);
                    m_MyGarage.SetWheels(i_LicensePlateNumber, manufacturerName, curAirPressure);
                    isSuccess = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    ChatBot.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
            }
        }

        private void SetEnergyType(string i_LicensePlateNumber)
        {
            bool isSuccess = false;
            float energyAmount = 0;
            while (!isSuccess)
            {
                try
                {
                    if (this.m_MyGarage.IsFuelBasedVehicle(i_LicensePlateNumber))
                    {
                        energyAmount = ChatBot.GetFuelCurStatus(i_LicensePlateNumber);
                    }
                    else
                    {
                        energyAmount = ChatBot.GetBatteryCurStatus(i_LicensePlateNumber);
                    }

                    this.m_MyGarage.SetEnergy(i_LicensePlateNumber, energyAmount);
                    isSuccess = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    ChatBot.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
            }
        }

        internal void ChangeVehicleStatus()
        {
            string licensePlateNumber = GetLicensePlateNumber();
            string newStatusAsString;
            eStatusInGarage newStatus;

            try
            {
                newStatusAsString = ChatBot.GetUpdatedStatus(licensePlateNumber);
                eStatusInGarage.TryParse(newStatusAsString, out newStatus);
                m_MyGarage.ChangeVehicleStatus(licensePlateNumber, newStatus);
            }
            catch (VehicleNotInGarageException e)
            {
                ChatBot.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        internal void InflateWheels()
        {
            string licensePlateNumber = GetLicensePlateNumber();
            int numberOfWheels = 0;
            float[] wheelsAirPressure;

            try
            {
                numberOfWheels = this.m_MyGarage.GetNumOfWheelsInVehicle(licensePlateNumber);
                wheelsAirPressure = new float[numberOfWheels];
                for (int i = 0; i < wheelsAirPressure.Length; i++)
                {
                    wheelsAirPressure[i] = this.r_FillAirToMax;
                }

                this.m_MyGarage.FillAirInWheels(licensePlateNumber, wheelsAirPressure);
            }
            catch (VehicleNotInGarageException e)
            {
                ChatBot.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
            catch (ValueOutOfRangeException e)
            {
                ChatBot.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
            }
        }

        internal void GetVehicleDetails()
        {
            string licensePlateNumber = GetLicensePlateNumber();
            string vehicleInfo;

            try
            {
                vehicleInfo = this.m_MyGarage.GetVehicleDetails(licensePlateNumber);
                Console.WriteLine(vehicleInfo);
                Console.WriteLine();
                Console.WriteLine("To go back to the main menu - press any key");
                Console.ReadLine();
                Console.Clear();
            }
            catch (VehicleNotInGarageException e)
            {
                ChatBot.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        internal void PrintLicencePlatesInGarageByFilter()
        {
            bool[] filters = new bool[3]; //Each index represents a status in garage - 0: treatment, 1: fixed, 2:payed
            string filteredLicensePlates;

            ChatBot.ChooseFilters(ref filters);
            filteredLicensePlates = m_MyGarage.GetLicensePlatesByState(ref filters);
            Console.WriteLine(filteredLicensePlates);
        }
    }
}

