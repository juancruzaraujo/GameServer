using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleOutputFormater;

namespace GameServerGateway
{
    public class MessageParams
    {
        string _message;
        bool _msgOk;
        bool _msgError;
        OutputFormatter _outputFormatter;
        


        public MessageParams SetMessage(string message)
        {
            _message = message;
            return this;
        }

        public MessageParams SetMessageOK(bool msgOk)
        {
            _msgOk = msgOk;
            return this;
        }

        public MessageParams SetMessageError(bool msgError)
        {
            _msgError = msgError;
            return this;
        }

        public MessageParams SetOutputFormatter(OutputFormatterAttributes outputFormatterAttributes)
        {
            
            //_outputFormatter = outputFormatter;
            return this;
        }

       
    }
}
