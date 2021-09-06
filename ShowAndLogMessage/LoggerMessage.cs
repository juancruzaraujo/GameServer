using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerFW;
using ConsoleOutputFormater;

namespace ShowAndLogMessage
{
    public class LoggerMessage
    {


        #if DEBUG
            private static bool debug_Mode = true;
            const int C_MAX_LOG_LINES_TO_SAVE = 0;
        #else
            private /*static*/ bool debug_Mode = false;
            const int C_MAX_LOG_LINES_TO_SAVE = 20;         
        #endif

        const string C_OK = " OK ";
        const string C_ERROR = " ERROR ";
        const string C_WARING = " WARNIG ";
        const string C_NO_TYPE = "";

        public enum typeMsg
        {
            OK,
            ERROR,
            WARNING,
            NO_TYPE
        }

        GameServerManager _gameServerManager;
        private FormatterOutput _formatterOutput;
        static LoggerMessage _loggerMessageInstance;
        int _logCounterLineSave;
        

        private LoggerMessage()
        {
            _formatterOutput = FormatterOutput.GetInstance();
        }

        public static LoggerMessage GetInstance()
        {
            if (_loggerMessageInstance == null)
            {
                _loggerMessageInstance = new LoggerMessage();
            }

            return _loggerMessageInstance;
        }

        public void CreateLogFile(GameServerManager gameServerManager)
        {
            _gameServerManager = gameServerManager;

            string fileName = _gameServerManager.GetConfig.serverConfig.serverParameters.logFileName;
            string path = _gameServerManager.GetConfig.serverConfig.serverParameters.logPathFile;

            _gameServerManager.CreateLogFile(fileName, path);

        }

        public void LogMessage(string message)
        {
            if (_logCounterLineSave < C_MAX_LOG_LINES_TO_SAVE)
            {
                _gameServerManager.WriteLog(message);
                _logCounterLineSave++;
            }
            else
            {
                _gameServerManager.WriteAndSaveLog(message);
                _logCounterLineSave = 0;
            }
            //hacer una clase estatica que devuelva el mensaje con sus colores            
        }


        public void ShowAndLogMessage(string message, OutputFormatterAttributes outputFormaterParam = null, typeMsg msgType = typeMsg.NO_TYPE)
        {
            string typeMsgFormatter = ""; //"[ " + typeMsg + " ]";
            string typeMsgStr = GetMsgTypeSTR(msgType);
            if (typeMsgStr != "")
            {
                typeMsgFormatter = "[ " + typeMsgStr + " ] ";
            }

            LogMessage(typeMsgFormatter + message);
            ShowMessage(message, outputFormaterParam, msgType);

        }

        public void ShowMessage(string message, OutputFormatterAttributes outputFormaterParam = null, typeMsg msgType = typeMsg.NO_TYPE)
        {
            //LogMessage(message);
            string msgFormatter = "";
            string typeMsgStr = GetMsgTypeSTR(msgType);

            if (typeMsgStr == C_OK)
            {
                msgFormatter = _formatterOutput.OkMessage() + FormattMSG(message, outputFormaterParam);
            }
            else if (typeMsgStr == C_WARING)
            {
                msgFormatter = _formatterOutput.WarnigMessage() + FormattMSG(message, outputFormaterParam);
            }
            else if (typeMsgStr == C_ERROR)
            {
                msgFormatter = _formatterOutput.ErrorMessage() + FormattMSG(message, outputFormaterParam);

            }
            else if (typeMsgStr == C_NO_TYPE)
            {
                msgFormatter = _formatterOutput.GetForrmatedText(message, outputFormaterParam);
            }

            _formatterOutput.ShowMessage(msgFormatter);
        }

        private string FormattMSG(string message, OutputFormatterAttributes outputFormaterParam)
        {
            if (outputFormaterParam != null)
            {
                message = _formatterOutput.GetForrmatedText(message, outputFormaterParam);
            }

            return message;
        }

        /// <summary>
        /// return [ custom type ], example, [ yupi! ]
        /// </summary>
        /// <param name="customType">custom type</param>
        /// <param name="outputFormaterParam">text atributes</param>
        /// <returns>return [ custom type ], example, [ yupi! ]</returns>
        public string CustomType(string customType, OutputFormatterAttributes outputFormaterParam)
        {
            return "[ " + _formatterOutput.GetForrmatedText(customType, outputFormaterParam) + " ]";
        }

        private string GetMsgTypeSTR(typeMsg msgType)
        {
            string res = "";

            switch(msgType)
            {
                case typeMsg.OK:
                    res = C_OK;
                    break;

                case typeMsg.WARNING:
                    res = C_WARING;
                    break;

                case typeMsg.ERROR:
                    res = C_ERROR;
                    break;

                case typeMsg.NO_TYPE: //pongo esto por prologidad
                    res = "";
                    break;
            }

            return res;
        }
    }
}
