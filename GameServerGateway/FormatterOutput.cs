using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerFW;
using ConsoleOutputFormater;

namespace GameServerGateway
{
    internal class FormatterOutput
    {
        static FormatterOutput _formaterOutputInstance;
        static OutputFormatter _outputFormater;

        private FormatterOutput()
        {
            _outputFormater = new OutputFormatter();
        }

        internal FormatterOutput GetInstance()
        {
            if (_formaterOutputInstance == null)
            {
                _formaterOutputInstance = new FormatterOutput();
            }
            return _formaterOutputInstance;
        }

        //tiene que retornan [ ERROR ]
        internal string ErrorMessage(string message)
        {
            return "";
        }

        //tiene que retornal [ OK ] mensaje
        internal string OkMessage(string message)
        {
            return "";
        }

        //ver como pasar el resto de parametros y cosas para mostrar mensajines
        internal string CustomMessage(string message,OutputFormatterAttributes outputFormatterAttributes)
        {
            //_outputFormater.FormatText()
            return "";
        }

    }
}
