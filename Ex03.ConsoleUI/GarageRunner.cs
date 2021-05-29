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
                    nextInstruction = ChatBot.GetInstructionFromUser();
                    switch (nextInstruction)
                    {
                        case 1:
                            this.m_Manager.AddNewVehicle();
                            ChatBot.PrintUpdateSuccessfully(eInstructionOption.AddNewVehicle);
                            break;
                        case 2:
                            this.m_Manager.FillEnergyInVehicle();
                            ChatBot.PrintUpdateSuccessfully(eInstructionOption.FuelOrChargeVehicle);
                            break;
                        case 3:
                            this.m_Manager.CheckVehicleState();
                            break;
                        case 4:
                            this.m_Manager.ChangeVehicleState();
                            ChatBot.PrintUpdateSuccessfully(eInstructionOption.ChangeVehicleState);
                            break;
                        case 5:
                            this.m_Manager.InflateWheels();
                            ChatBot.PrintUpdateSuccessfully(eInstructionOption.InflateWheels);
                            break;
                        case 6:
                            this.m_Manager.GetVehicleDetails();
                            break;
                        case 7:
                            this.m_Manager.ShowLicencePlatesInGarageByFilter();
                            break;
                        default:
                            ChatBot.PrintBadChosenOptionMessage();
                            break;
                    }
                }
                catch (FormatException e)
                {
                    Printer.PrintMessage(e.Message);
                }
            }
        }
    }
}
