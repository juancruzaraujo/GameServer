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
        internal string ErrorMessage()
        {
            return DefaultMessages(C_ERROR);
        }

        /// <summary>
        /// return string [ OK ] + message
        /// </summary>
        /// <returns>string</returns>
        internal string OkMessage()
        {

            return DefaultMessages(C_OK);
        }

        /// <summary>
        /// message to show with [ WARNIG ] in front
        /// </summary>
        /// <returns>string</returns>
        internal string WarnigMessage()
        {
            return DefaultMessages(C_WARING);
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

            return "[" + result + "] ";
        }

        internal void ShowMessage(string message)
        {
            //Console.WriteLine(_outputFormater.FormatText(message, outputFormatterAttributes));
            Console.WriteLine(message);
        }

        internal string GetForrmatedText(string text, OutputFormatterAttributes outputFormaterParam)
        {
            return _outputFormater.FormatText(text, outputFormaterParam);
        }

    }
}
