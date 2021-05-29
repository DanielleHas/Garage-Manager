using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class ChatBot
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
            if (!ParsePhoneNumber(o_PhoneNumber, out string error))
            {
                throw new FormatException(error);
            }
        }

        private static bool ParsePhoneNumber(string i_PhoneNumber, out string o_Error)
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
                o_Error = "The number have to many digits";
            }
            else if (i_PhoneNumber.Length > 10)
            {
                successParsing = false;
                o_Error = "The number is missing digits";
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
            if (o_VehicleType != 3) //isn't a truck
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

        public static string GetLicensePlateNumber()
        {
            Console.WriteLine("Please enter the license plate number of the car ");
            string licensPlateNumber = getInputFromUser();
            if (!ParseLicensePlateNumber(licensPlateNumber, out string error))
            {
                throw new FormatException(error);
            }
            return licensPlateNumber;
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
