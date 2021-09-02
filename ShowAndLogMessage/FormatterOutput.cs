using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerFW;
using ConsoleOutputFormater;

namespace ShowAndLogMessage
{
    internal class FormatterOutput
    {

        internal const string C_OK = " OK ";
        internal const string C_ERROR = " ERROR ";
        internal const string C_WARING = " WARNIG ";


        static FormatterOutput _formaterOutputInstance;
        static OutputFormatter _outputFormater;

        private FormatterOutput()
        {
            _outputFormater = new OutputFormatter();
        }

        static internal FormatterOutput GetInstance()
        {
            if (_formaterOutputInstance == null)
            {
                _formaterOutputInstance = new FormatterOutput();
            }
            return _formaterOutputInstance;
        }

        /// <summary>
        /// message to show with [ ERROR ] in front
        /// </summary>
        /// <param name="message">message to show with [ ERROR ] in front</param>
        /// <returns>string</returns>
        internal string ErrorMessage(string message)
        {
            return DefaultMessages(C_ERROR) + " " + message;
        }

        /// <summary>
        /// return string [ OK ] + message
        /// </summary>
        /// <param name="message">message to show with [ OK ] in front</param>
        /// <returns>string</returns>
        internal string OkMessage(string message)
        {

            return DefaultMessages(C_OK) + " " + message;
        }

        /// <summary>
        /// message to show with [ WARNIG ] in front
        /// </summary>
        /// <param name="message">message to show with [ WARNIG ] in front</param>
        /// <returns>string</returns>
        internal string WarnigMessage(string message)
        {
            return DefaultMessages(C_WARING) + " " + message;
        }

        //ver como pasar el resto de parametros y cosas para mostrar mensajines
        internal string CustomMessage(string message,OutputFormatterAttributes outputFormatterAttributes)
        {
            return _outputFormater.FormatText(message, outputFormatterAttributes);
        }

        private string DefaultMessages(string msgType)
        {
            OutputFormatterAttributes outputFormatterAttributes = new OutputFormatterAttributes();
            outputFormatterAttributes.SetBold(true);
            string result = "";
                

            string okMessage = _outputFormater.FormatText(C_OK, outputFormatterAttributes);

            switch(msgType)
            {
                case C_OK:
                    outputFormatterAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Green);
                    result = _outputFormater.FormatText(C_OK, outputFormatterAttributes);
                    break;

                case C_WARING:
                    outputFormatterAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Yellow);
                    result = _outputFormater.FormatText(C_WARING, outputFormatterAttributes);
                    break;

                case C_ERROR:
                    outputFormatterAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Red);
                    result = _outputFormater.FormatText(C_ERROR, outputFormatterAttributes);
                    break;
            }

            outputFormatterAttributes.SetColorBG(OutputFormatterAttributes.TextColorBG.Black);

            return "[" + result + "]";
        }

        internal void ShowMessage(string message, OutputFormatterAttributes outputFormatterAttributes)
        {
            Console.WriteLine(_outputFormater.FormatText(message, outputFormatterAttributes));
        }

    }
}
