﻿using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class ChatBot
    {
        internal static int GreetUser()
        {

            Console.WriteLine("Hello! welcome to the Garage!");
            Console.WriteLine("You can preform the following operatins on your garage" + System.Environment.NewLine);

            int instruction = 0;
            Console.WriteLine("What whould you like to do?");
            PrintInstructionOptions();
            if (!int.TryParse(getInputFromUser(), out instruction) || instruction > 7 || instruction < 1)
            {
                throw new FormatException("Bad instructions input");
            }

            Console.Clear();

            return instruction;
        }

        private static void PrintInstructionOptions()
        {
            Console.WriteLine(string.Format(@" 
                             [1] - Add a new vehicle to the garage
                             [2] - Display list of license plates by filter
                             [3] - Change a certain vehicle’s status
                             [4] - Inflate the wheels of a vehicle
                             [5] - Refuel a fuel-based vehicle
                             [6] - Charge an electric-based vehicle
                             [7] - Display vehicle information"));
        }

        internal static void GetOwnerName(out string o_Name)
        {
            Console.WriteLine("Please enter the owner's name");
            o_Name = getInputFromUser();
        }

        internal static void GetOwnerPhoneNumber(string i_Name, out string o_PhoneNumber)
        {
            Console.WriteLine(string.Format("Please enter {0}'s phone number (10 digit number without punctuations)", i_Name));
            o_PhoneNumber = getInputFromUser();
            try {
                parsePhoneNumber(o_PhoneNumber, out string error);
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static bool parsePhoneNumber(string i_PhoneNumber, out string o_Error)
        {
            o_Error = null;
            bool successParsing = true;
            long parsedPhoneNumber;
            if (!long.TryParse(i_PhoneNumber, out parsedPhoneNumber))
            {
                successParsing = false;
                o_Error = "There is a problem with phone number format. Please only use a 10 digits number";
            }
            if (i_PhoneNumber.Length < 10)
            {
                successParsing = false;
                o_Error = "The number is missing digits";
            }
            else if (i_PhoneNumber.Length > 10)
            {
                successParsing = false;
                o_Error = "The number have to many digits";
            }
            return successParsing;
        }

        internal static void GetVehicleGeneralDetails()
        {
            char isElectricOrFuel = 'F';

            Console.Clear();
            Console.WriteLine("Please provide the car details:");
            Console.WriteLine();
            int o_VehicleType = getVehicleType();
            Console.WriteLine(string.Format("What is the {0}'s model?", (eVehicleTypes)o_VehicleType));
            string o_ModelName = getInputFromUser();
            char electricOrFuel = ' '; // temp variable
            if (o_VehicleType != 2) //isn't a truck
            {
                Console.WriteLine(string.Format("Is the {0} electric? (press E for electric based and f for fuel based)", (eVehicleTypes)o_VehicleType));
                electricOrFuel = getInputFromUser()[0];
                while (electricOrFuel != 'F' && electricOrFuel != 'f' && electricOrFuel != 'E' && electricOrFuel != 'e')
                {
                    Console.Clear();
                    Console.WriteLine("Please choose E for electric and F for fuled");
                    electricOrFuel = getInputFromUser()[0];
                }
            }

            if (electricOrFuel == 'E' || electricOrFuel == 'e')
            {
                isElectricOrFuel = 'E';
            }

            string o_LicensePlateNumber = GetLicensePlateNumber();
        }

        internal static void PrintStatus(eStatusInGarage vehicleState, string licensePlateNumber)
        {
            Console.WriteLine(string.Format("The status of car number {0} is {1} ", licensePlateNumber, vehicleState));
        }

        internal static void PrintVehicleAlreadyInGarage(string i_LicensePlateNumber)
        {
            Console.WriteLine(string.Format("Vehicle {0} is already listed in the garage. Moved to fixing state.", i_LicensePlateNumber));

        }

        internal static void GetVehicleGeneralDetails(out int o_VehicleType, out string o_ModelName, out char o_IsElectric, out string o_LicensePlateNumber)
        {
            char isElectricOrFuel = 'F'; //F for fuel, E for electric

            Console.Clear();
            Console.WriteLine("Let's get some details about the vehicle.");
            o_VehicleType = getVehicleType();
            Console.WriteLine(string.Format("What is the {0}'s model?", (eVehicleTypes)o_VehicleType));
            o_ModelName = getInputFromUser();
            if (o_VehicleType != 3)
            {
                Console.WriteLine(string.Format("Is the {0} electric? Please choose E for electric and F for fuled", (eVehicleTypes)o_VehicleType));
                isElectricOrFuel = getInputFromUser()[0];
                while (isElectricOrFuel != 'F' && isElectricOrFuel != 'f' && isElectricOrFuel != 'E' && isElectricOrFuel != 'e')
                {
                    Console.Clear();
                    Console.WriteLine("Please choose E for electric and F for fuled");
                    isElectricOrFuel = getInputFromUser()[0];
                }
            }

            o_IsElectric = isElectricOrFuel;
            o_LicensePlateNumber = GetLicensePlateNumber();
        }

        internal static Dictionary<string, string> GetExtraFeatures(int i_VehicleType)
        {
            Dictionary<string, string> o_ExtraFeatursDictionary = new Dictionary<string, string>();
            switch (i_VehicleType)
            {
                case 1: // car
                    o_ExtraFeatursDictionary.Add("Color", getColor());
                    o_ExtraFeatursDictionary.Add("Number of doors", getNumOfDoors());
                    break;
                case 2: //motorcycle
                    o_ExtraFeatursDictionary.Add("Licens type", getLicensType());
                    o_ExtraFeatursDictionary.Add("Energy capacity", getEnergyCapacity());
                    break;
                case 3: //Truck
                    o_ExtraFeatursDictionary.Add("Has dangerous materials", getHasDangerousMaterials());
                    o_ExtraFeatursDictionary.Add("Maximum carring weight", getMaximumCarringWeight());
                    break;
            }
            return o_ExtraFeatursDictionary;
        }

        private static string getMaximumCarringWeight()
        {
            bool v_ValidCarringWeight = false;
            string carringWeightAsString = "";
            Console.WriteLine("Please enter the Maximum Carring Weight - ");
            while (!v_ValidCarringWeight)
            {
                carringWeightAsString = getInputFromUser();
                float o_CarringWeightAsFloat;
                bool isSucceed = float.TryParse(carringWeightAsString, out o_CarringWeightAsFloat);
                if (!isSucceed)
                {
                    Console.WriteLine("Invalid Number. Please enter again:");
                }
                else
                {
                    v_ValidCarringWeight = true;
                }
            }
            return carringWeightAsString;
        }

        private static string getHasDangerousMaterials()
        {
            bool v_ValidInput = false;
            string inputAsString = "";
            Console.WriteLine("Do you carry dangeraus materials? (Y / N) - ");
            while (!v_ValidInput)
            {
                inputAsString = getInputFromUser();
                if (inputAsString.ToUpper() != "Y" && inputAsString.ToUpper() != "N")
                {
                    Console.WriteLine("Invalid input. Please enter again:");
                }
                else
                {
                    inputAsString = inputAsString.ToUpper();
                    v_ValidInput = true;
                }
            }
            return inputAsString;
        }

        private static string getEnergyCapacity()
        {
            bool v_ValidEnergyCapacity = false;
            string energyCapacityAsString = "";
            Console.WriteLine("Please enter the energy capacity - ");
            while (!v_ValidEnergyCapacity)
            {
                energyCapacityAsString = getInputFromUser();
                int o_EnergyCapacityAsInt;
                bool isSucceed = Int32.TryParse(energyCapacityAsString, out o_EnergyCapacityAsInt);
                if (!isSucceed)
                {
                    Console.WriteLine("Invalid Number. Please enter again:");
                }
                else
                {
                    v_ValidEnergyCapacity = true;
                }
            }
            return energyCapacityAsString;
        }

        private static string getLicensType()
        {
            bool v_ValidLicenseType = false;
            int licensTypeIndex = 1;
            Console.WriteLine("Please enter your license type - ");
            while (!v_ValidLicenseType)
            {
                Console.WriteLine(string.Format(@" 
                             [1] - A
                             [2] - B1
                             [3] - AA
                             [4] - BB"));
                string licensTypeAsString = getInputFromUser();
                licensTypeIndex = Int32.Parse(licensTypeAsString);
                if (licensTypeIndex < 1 || licensTypeIndex > 4)
                {
                    Console.WriteLine("Invalid Number. Please enter again:");
                }
                else
                {
                    v_ValidLicenseType = true;
                }
            }
            return ((eLicensTypes)(licensTypeIndex)).ToString();
        }

        private static string getNumOfDoors()
        {
            bool v_ValidDoorsNumber = false;
            int doorsNumber = 2;
            Console.WriteLine("Please enter the number of doors in the car - ");
            while (!v_ValidDoorsNumber)
            {
                Console.WriteLine(string.Format(@" 
                             [2] - 2
                             [3] - 3
                             [4] - 4
                             [5] - 5"));
                string doorsNumberAsString = getInputFromUser();
                doorsNumber = Int32.Parse(doorsNumberAsString);
                if (doorsNumber < 2 || doorsNumber > 5)
                {
                    Console.WriteLine("Invalid Number. Please enter again:");
                }
                else
                {
                    v_ValidDoorsNumber = true;
                }
            }
            return ((eNumsOfDoors)(doorsNumber)).ToString();
        }

        private static string getColor()
        {
            bool v_ValidColor = false;
            int colorIndex = 1;
            Console.WriteLine("Please enter your car color plate number of the car - ");
            while(!v_ValidColor)
            {
                Console.WriteLine(string.Format(@" 
                             [1] - Red
                             [2] - Silver
                             [3] - White
                             [4] - Black"));
                string colorAsNumber = getInputFromUser();
                colorIndex = Int32.Parse(colorAsNumber);
                if (colorIndex < 1 || colorIndex > 4)
                {
                    Console.WriteLine("There is no such color. Please enter again:");
                }
                else
                {
                    v_ValidColor = true;
                }
            }
            return ((eColors)(colorIndex)).ToString();            
        }

        internal static string GetLicensePlateNumber()
        {
            Console.WriteLine("Please enter the license plate number of the car ");
            string licensPlateNumber = getInputFromUser();
            if (!ParseLicensePlateNumber(licensPlateNumber, out string error))
            {
                throw new FormatException(error);
            }
            return licensPlateNumber;
        }

        internal static void PrintLicensePlateNotFoundMessage(string i_LicensePlateNumber)
        {
            Console.WriteLine(string.Format("A vehicle with a license plate number matching your input - {0} does not exist in this garage.", i_LicensePlateNumber));
        }

        private static bool ParseLicensePlateNumber(string licensePlateNumber, out string o_Error)
        {
            o_Error = null;
            bool successParsing = true;
            long parsedLicensePlate;
            if (!long.TryParse(licensePlateNumber, out parsedLicensePlate))
            {
                successParsing = false;
                o_Error = "There is a problem with The plate number, please enter only digits";
            }

            return successParsing;
        }

        private static int getVehicleType()
        {
            string vehicleTypeCode;
            int vehicleTypeNumber = 0;
            eVehicleTypes vehicleType;

            Console.WriteLine("What type of vehicle are you registering?");
            PrintVehicleTypesOptions();
            vehicleTypeCode = getInputFromUser();
            while (!eVehicleTypes.TryParse(vehicleTypeCode, out vehicleType))
            {
                Console.WriteLine("Invalid input. Try again");
                PrintVehicleTypesOptions();
                vehicleTypeCode = getInputFromUser();
            }

            vehicleTypeNumber = int.Parse(vehicleTypeCode);

            return vehicleTypeNumber;
        }

        private static void PrintVehicleTypesOptions()
        {
            Console.WriteLine(string.Format(@" 
                             [1] - Car
                             [2] - Motorcycle
                             [3] - Truck"));
        }
        internal static float GetBatteryCurrentStatus(string i_LicensePlateNumber)
        {
            float currentBatteryChargeInHours = 0;

            Console.WriteLine(string.Format("What is the current battery charge of vehicle number {0}? (in hours)", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out currentBatteryChargeInHours))
            {
                Console.WriteLine("Invalid input. Please enter the battery charge (in hours).");
            }

            return currentBatteryChargeInHours;
        }

        internal static float GetFuelTankCurrentStatus(string i_LicensePlateNumber)
        {
            float currentFuleTankStatusInLiters = 0;

            Console.WriteLine(string.Format("What is the current tank status of vehicle {0}? (in liters)", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out currentFuleTankStatusInLiters))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter the tank status (in liters) again.");
            }

            return currentFuleTankStatusInLiters;
        }

        internal static float GetChargingDetails(string i_LicensePlateNumber)
        {
            float chargeAmmount = 0;

            Console.WriteLine(string.Format("How much time do you want to charge vehicle {0}? (in hours)", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out chargeAmmount))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter the wanted charge time (in hours) again.");
            }

            return chargeAmmount;
        }

        internal static void GetFuelingDetails(string i_LicensePlateNumber, out float o_FuelAmount, out string o_FuelType)
        {
            float fuelAmount = 0;

            o_FuelType = GetFuelType();
            Console.WriteLine(string.Format("How many liters do you want to fuel into vehicle {0} with?", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out fuelAmount))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter the wanted fuel amount (in liters) again.");
            }

            o_FuelAmount = fuelAmount;
        }

        internal static string GetFuelType()
        {
            string fuelType;
            int fuelTypeCode = 0;

            Console.WriteLine("What is the fuel type that you whould like to use?");
            PrintFuelTypeOptions();
            fuelType = getInputFromUser();
            while (!int.TryParse(fuelType, out fuelTypeCode) || fuelTypeCode < 1 || fuelTypeCode > 4)
            {
                throw new FormatException("Bad fuel type input.");
            }

            return fuelType;
        }

        private static void PrintFuelTypeOptions()
        {
            Console.WriteLine(String.Format(@"[1] - Octan95
                                              [2] - Octan96 
                                              [3] - Octan98
                                              [4] - Soler"));
        }

        internal static void GetWheelsManufacturer(string i_LicensePlateNumber, out string o_ManufacturerName)
        {
            Console.Clear();
            Console.WriteLine(string.Format("What is the manufacturer's name for the wheels of vehicle {0}?", i_LicensePlateNumber));
            o_ManufacturerName = getInputFromUser();
        }

        internal static float GetCurrentAirPressure(string i_LicensePlateNumber)
        {
            float currenAirPressure = 0;

            Console.WriteLine(string.Format("What is the air pressure of the wheels in vehicle number {0}?", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out currenAirPressure))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter the current air pressure again.");
            }

            return currenAirPressure;
        }

        internal static void ChooseFilters(ref bool[] io_FilterChart)
        {
            char userAnswer;
            eStatusInGarage[] states = { eStatusInGarage.Treatment, eStatusInGarage.Fixed, eStatusInGarage.Payed };

            Console.Clear();
            for (int i = 0; i < states.Length; i++)
            {
                Console.WriteLine(string.Format("Would you like to see a list of the {0} cars? Y\\N", states[i]));
                userAnswer = getInputFromUser()[0];
                while (userAnswer != 'Y' && userAnswer != 'N' && userAnswer != 'n' && userAnswer != 'y')
                {
                    Console.WriteLine(string.Format("Invalid input. Please enter 'Y' if you would like to see {0}, and 'N' if you would't", states[i]));
                    Console.WriteLine(string.Format("Would you like to see a list of the {0} cars? Y\\N", states[i]));
                    userAnswer = getInputFromUser()[0];
                }

                io_FilterChart[i] = (userAnswer == 'Y' || userAnswer == 'y');
            }
        }

        internal static string GetUpdatedState(string i_LicensePlateNumber)
        {
            string newState;
            int newStateCode = 0;

            Console.Clear();
            Console.WriteLine(string.Format("What is the new state of vehicle {0}", i_LicensePlateNumber));
            PrintStateOptions();
            newState = getInputFromUser();
            while (!int.TryParse(newState, out newStateCode) || newStateCode < 1 || newStateCode > 3)
            {
                Console.WriteLine("Invalid input. Please select the number corresponding to the chosen option:");
                PrintStateOptions();
                newState = getInputFromUser();
                int.TryParse(newState, out newStateCode);
            }

            return newState;
        }

        internal static void PrintStateOptions()
        {
            Console.WriteLine("[1] - Ttreatment" + System.Environment.NewLine + "[2] - Fixed" + System.Environment.NewLine + "[3] - Payed" + System.Environment.NewLine);
        }

        internal static void PrintValueOutOfRangeMessage(float i_MaxValue, float i_MinValue)
        {
            Console.WriteLine(string.Format("The value you entered is not in the range, please enter a number between {0} and {1}", i_MinValue, i_MaxValue));
        }

        internal static void PrintFuelTypeErrorMessage(eFuelTypes i_FuelType)
        {
            Console.WriteLine(string.Format("The fuel type {0} does not match this vehicle type. Try again", i_FuelType));
        }

        private static string getInputFromUser()
        {
            string input = Console.ReadLine();

            while (input == null)
            {
                Console.WriteLine("You didn't enter anything... Try again.");
                input = Console.ReadLine();
            }

            Console.Clear();

            return input;
        }
    }
}
