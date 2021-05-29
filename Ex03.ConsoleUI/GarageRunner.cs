using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class GarageRunner
    {
        private GarageManager m_Manager;

        internal void Run()
        {
            ChatBot chatbot;
            int instructionNum = 0;
            this.m_Manager = new GarageManager();

            while (true)
            {
                try
                {
                    instructionNum = ChatBot.GreetUser();
                    switch (instructionNum)
                    {
                        case 1:
                            this.m_Manager.AddNewVehicle();
                            break;
                        case 2:
                            this.m_Manager.ShowLicencePlatesInGarageByFilter();
                            break;
                        case 3:
                            this.m_Manager.ChangeVehicleState();
                            break;
                        case 4:
                            this.m_Manager.InflateWheels();
                            break;
                        case 5:
                            this.m_Manager.FillEnergyInVehicle(); 
                            break;
                        case 6:
                            this.m_Manager.FillEnergyInVehicle();
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
