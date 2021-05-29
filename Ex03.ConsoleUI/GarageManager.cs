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

        internal void FillEnergyInVehicle()
        {
            bool chargedSuccessfully = false;
            bool isElectricVehicle;
            float amountToFill = 0;
            eFuelTypes fuelType;
            string fuelTypeCode;
            string licensePlateNumber = getLicensePlateNumber();

            while (!chargedSuccessfully)
            {
                try
                {
                    isElectricVehicle = this.m_MyGarage.CheckIfElectricByLicensePlate(licensePlateNumber);
                    if (isElectricVehicle)
                    {
                        amountToFill = ChatBot.GetChargingDetails(licensePlateNumber);
                        this.m_MyGarage.FillEnergySource(licensePlateNumber, amountToFill);
                    }
                    else
                    {
                        ChatBot.GetFuelingDetails(licensePlateNumber, out amountToFill, out fuelTypeCode);
                        eFuelTypes.TryParse(fuelTypeCode, out fuelType);
                        this.m_MyGarage.FillEnergySource(licensePlateNumber, amountToFill, fuelType);
                    }

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
                eStatusInGarage vehicleState = this.m_MyGarage.GetStateByLicensePlate(licensePlateNumber);
                Printer.PrintState(vehicleState, licensePlateNumber);
            }
            catch (VehicleNotInGarageException e)
            {
                ChatBot.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        internal void AddNewVehicle()
        {
            int vehicleType = 0;
            string modelName;
            string licensePlateNumber;
            string ownerName;
            string ownerPhoneNumber;
            bool isElectric;
            Dictionary<string, string> vehicleSpecialFeatures;

            ChatBot.GetVehicleGeneralDetails(out vehicleType, out modelName, out isElectric, out licensePlateNumber);
            getAndSetOwnerNameAndPhoneNumber(out ownerName, out ownerPhoneNumber);
            vehicleSpecialFeatures = this.m_MyGarage.AddVehicle(vehicleType, modelName, isElectric, licensePlateNumber, ownerName, ownerPhoneNumber);
            getAndSetEnergySource(licensePlateNumber);
            getAndSetWheels(licensePlateNumber);
            if (vehicleSpecialFeatures == null)
            {
                ChatBot.PrintVehicleAlreadyInGarage(licensePlateNumber);
            }
            else
            {
                getAndSetSpecialFeatures(licensePlateNumber, ref vehicleSpecialFeatures);
            }
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

        private void getAndSetSpecialFeatures(string i_LicensePlateNumber, ref Dictionary<string, string> io_VehicleSpecialFeatures)
        {
            bool specialFeaturesSetSuccessfully = false;

            ChatBot.GetVehicleSpecificDetails(ref io_VehicleSpecialFeatures);
            while (!specialFeaturesSetSuccessfully)
            {
                try
                {
                    this.m_MyGarage.SetVehicleSpecialFeatures(i_LicensePlateNumber, ref io_VehicleSpecialFeatures);
                    specialFeaturesSetSuccessfully = true;
                }
                catch (ArgumentException e)
                {
                    string[] errorMessages = e.Message.Split(':');
                    ChatBot.PrintArgumentException(errorMessages[1]);
                    ChatBot.GetVehicleSpecificDetail(ref io_VehicleSpecialFeatures, errorMessages[0]);
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
                    if (this.m_MyGarage.CheckIfElectricByLicensePlate(i_LicensePlateNumber))
                    {
                        energyAmountInEnergySource = ChatBot.GetBatteryCurrentStatus(i_LicensePlateNumber);
                    }
                    else
                    {
                        energyAmountInEnergySource = ChatBot.GetFuelTankCurrentStatus(i_LicensePlateNumber);
                    }

                    this.m_MyGarage.SetEnergySource(i_LicensePlateNumber, energyAmountInEnergySource);
                    setSuccessfuly = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    ChatBot.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
            }
        }

        internal void ChangeVehicleState()
        {
            string licensePlateNumber = getLicensePlateNumber();
            string newStateCode;
            eStatusInGarage newState;

            try
            {
                newStateCode = ChatBot.GetUpdatedState(licensePlateNumber);
                eStatusInGarage.TryParse(newStateCode, out newState);
                m_MyGarage.ChangeVehicleStatus(licensePlateNumber, newState);
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
                numberOfWheels = this.m_MyGarage.GetNumberOfWheelsInSpecificVehicle(licensePlateNumber);
                wheelsAirPressure = new float[numberOfWheels];
                for (int i = 0; i < wheelsAirPressure.Length; i++)
                {
                    wheelsAirPressure[i] = this.r_FillAirToMaxCode;
                }

                this.m_MyGarage.PumpWheels(licensePlateNumber, wheelsAirPressure);
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
            filteredLicensePlates = m_MyGarage.LicensePlatesByState(ref filters);
            Console.WriteLine(filteredLicensePlates);
        }
    }
}

