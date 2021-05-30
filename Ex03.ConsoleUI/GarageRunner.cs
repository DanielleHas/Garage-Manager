using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class GarageRunner
    {
        private GarageManager m_Manager;

        internal void Run()
        {
            int instructionIndex = 0;
            this.m_Manager = new GarageManager();

            while (true)
            {
                try
                {
                    instructionIndex = ChatBot.GreetUser();
                    switch (instructionIndex)
                    {
                        case 1:
                            this.m_Manager.AddNewVehicle();
                            break;
                        case 2:
                            this.m_Manager.PrintLicencePlatesInGarageByFilter();
                            break;
                        case 3:
                            this.m_Manager.ChangeVehicleStatus();
                            break;
                        case 4:
                            this.m_Manager.InflateWheels();
                            break;
                        case 5:
                            this.m_Manager.FillEnergyInVehicle(true); // For fuel
                            break;
                        case 6:
                            this.m_Manager.FillEnergyInVehicle(false); // For electric
                            break;
                        case 7:
                            this.m_Manager.GetVehicleDetails();
                            break;
                        default:
                            Console.WriteLine("You didn't choose a valid option");
                            break;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
