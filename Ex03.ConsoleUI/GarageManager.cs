using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class GarageManager
    {
        private Garage m_MyGarage;
        private readonly float r_FillAirToMaxCode = -1.0F;

        internal GarageManager()
        {
            this.m_MyGarage = new Garage();
        }

        private string getLicensePlateNumber()
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
            string licensePlateNumber = getLicensePlateNumber();

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

        internal void CheckVehicleState()
        {
            string licensePlateNumber = getLicensePlateNumber();

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

        internal void AddNewVehicle()
        {
            int o_VehicleType = 0;
            string o_ModelName;
            string o_LicensePlateNumber;
            string o_OwnerName;
            string o_OwnerPhoneNumber;
            char o_IsFuelBased;

            ChatBot.GetVehicleGeneralDetails(out o_VehicleType, out o_ModelName, out o_IsFuelBased, out o_LicensePlateNumber);
            getAndSetOwnerNameAndPhoneNumber(out o_OwnerName, out o_OwnerPhoneNumber);
            getAndSetEnergySource(o_LicensePlateNumber);
            bool v_IsFuelBased = o_IsFuelBased == 'F' || o_IsFuelBased == 'f' ? true : false;
            Dictionary<string, string> extraFeatursDictionary = getExtraFeatures(o_VehicleType);
            m_MyGarage.AddVehicle(o_VehicleType, o_ModelName, o_LicensePlateNumber, o_OwnerName, o_OwnerPhoneNumber, v_IsFuelBased, extraFeatursDictionary);
            getAndSetWheels(o_LicensePlateNumber);
        }

        private Dictionary<string, string> getExtraFeatures(int io_VehicleType)
        {
            Dictionary<string, string> extraFeatursDictionary = ChatBot.GetExtraFeatures(io_VehicleType);
            return extraFeatursDictionary;
        }

        private void getAndSetOwnerNameAndPhoneNumber(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            bool phoneNumberSetSuccessfully = false;

            o_OwnerPhoneNumber = "";
            ChatBot.GetOwnerName(out o_OwnerName);
            while (!phoneNumberSetSuccessfully)
            {
                try
                {
                    ChatBot.GetOwnerPhoneNumber(o_OwnerName, out o_OwnerPhoneNumber);
                    phoneNumberSetSuccessfully = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void getAndSetWheels(string i_LicensePlateNumber)
        {
            string manufacturerName;
            float currentAirPressure = 0;
            bool setSuccessfuly = false;

            ChatBot.GetWheelsManufacturer(i_LicensePlateNumber, out manufacturerName);
            while (!setSuccessfuly)
            {
                try
                {
                    currentAirPressure = ChatBot.GetCurrentAirPressure(i_LicensePlateNumber);
                    m_MyGarage.SetWheels(i_LicensePlateNumber, manufacturerName, currentAirPressure);
                    setSuccessfuly = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    ChatBot.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
            }
        }

        private void getAndSetEnergySource(string i_LicensePlateNumber)
        {
            bool setSuccessfuly = false;
            float energyAmountInEnergySource = 0;

            while (!setSuccessfuly)
            {
                try
                {
                    if (this.m_MyGarage.IsFuelBasedVehicle(i_LicensePlateNumber))
                    {
                        energyAmountInEnergySource = ChatBot.GetFuelTankCurrentStatus(i_LicensePlateNumber);
                    }
                    else
                    {
                        energyAmountInEnergySource = ChatBot.GetBatteryCurrentStatus(i_LicensePlateNumber);
                    }

                    this.m_MyGarage.SetEnergy(i_LicensePlateNumber, energyAmountInEnergySource);
                    setSuccessfuly = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    ChatBot.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
            }
        }

        internal void ChangeVehicleStatus()
        {
            string licensePlateNumber = getLicensePlateNumber();
            string newStatusCode;
            eStatusInGarage newStatus;

            try
            {
                newStatusCode = ChatBot.GetUpdatedStatus(licensePlateNumber);
                eStatusInGarage.TryParse(newStatusCode, out newStatus);
                m_MyGarage.ChangeVehicleStatus(licensePlateNumber, newStatus);
            }
            catch (VehicleNotInGarageException e)
            {
                ChatBot.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        internal void InflateWheels()
        {
            string licensePlateNumber = getLicensePlateNumber();
            int numberOfWheels = 0;
            float[] wheelsAirPressure;

            try
            {
                numberOfWheels = this.m_MyGarage.GetNumOfWheelsInVehicle(licensePlateNumber);
                wheelsAirPressure = new float[numberOfWheels];
                for (int i = 0; i < wheelsAirPressure.Length; i++)
                {
                    wheelsAirPressure[i] = this.r_FillAirToMaxCode;
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
            string licensePlateNumber = getLicensePlateNumber();
            string vehicleInfo;

            try
            {
                vehicleInfo = this.m_MyGarage.GetVehicleDetails(licensePlateNumber);
                Console.WriteLine(vehicleInfo);
            }
            catch (VehicleNotInGarageException e)
            {
                ChatBot.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        internal void ShowLicencePlatesInGarageByFilter()
        {
            bool[] filters = new bool[3];
            string filteredLicensePlates;

            ChatBot.ChooseFilters(ref filters);
            filteredLicensePlates = m_MyGarage.GetLicensePlatesByState(ref filters);
            Console.WriteLine(filteredLicensePlates);
        }
    }
}

