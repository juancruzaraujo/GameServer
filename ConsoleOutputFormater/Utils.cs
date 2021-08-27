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
            OutputFormatter outputFormater = new OutputFormatter();

            string aux = outputFormater.FormatText("OK", OutputFormatter.TextColorFG.Bright_Green, OutputFormatter.TextColorBG.Black);
            return("[ " + aux + " ]");

        }

        public string ReturnError()
        {
            OutputFormatter outputFormater = new OutputFormatter();

            string aux = outputFormater.FormatText("ERROR", OutputFormatter.TextColorFG.Bright_Red, OutputFormatter.TextColorBG.Black);
            return ("[ " + aux + " ]");

        }

    }
}
