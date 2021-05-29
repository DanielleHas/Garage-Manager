using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            GarageLogic.Garage myGarage = new GarageLogic.Garage(new Dictionary(), );
            ChatBot.GreetUser();
        }
    }
}
