using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleOutputFormater
{

    /// <summary>
    /// OutputFormatter v1
    /// </summary>
    public class OutputFormatter
    {
        

        //Console.WriteLine("\x1b[97;101m TEST SERVER.\r\n");

       

        public OutputFormatter()
        {
            
        }

        public string FormatText(string text, OutputFormatterAttributes outputFormatterAttributes=null)
        {
            //  "\x1b[1m bold.\x1b[0m  \r\n "
            //en caso de no haber definido ningún atributo el objeto inicial tiene por default el normal
            //letra blanca y fondo negro
            if (outputFormatterAttributes == null) 
            {
                outputFormatterAttributes = new OutputFormatterAttributes();
            }

            string aux = "\x1b[" + outputFormatterAttributes.GetAttributes + outputFormatterAttributes.GetTextFG + 
                OutputFormatterAttributes.C_P + outputFormatterAttributes.GetTextBG + OutputFormatterAttributes.C_M + text + "\x1b[0m";

            return aux;
        }

       
    }
}
