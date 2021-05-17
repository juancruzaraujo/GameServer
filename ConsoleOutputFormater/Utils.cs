using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleOutputFormater;

namespace ConsoleOutputFormater
{
    public class Utils
    {
        public string ReturnOk()
        {
            OutputFormater outputFormater = new OutputFormater();

            string aux = outputFormater.FormatText("OK", OutputFormater.TextColorFG.Bright_Green, OutputFormater.TextColorBG.Black);
            return("[ " + aux + " ]");

        }

        public string ReturnError()
        {
            OutputFormater outputFormater = new OutputFormater();

            string aux = outputFormater.FormatText("ERROR", OutputFormater.TextColorFG.Bright_Red, OutputFormater.TextColorBG.Black);
            return ("[ " + aux + " ]");

        }

    }
}
