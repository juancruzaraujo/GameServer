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

        public const string C_OK = " OK ";
        public const string C_ERROR = " ERROR ";
        public const string C_WARING = " WARNIG ";

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


        public void ShowAndLogMessage(string message, OutputFormatterAttributes outputFormaterParam = null, string typeMsg = "")
        {
            string typeMsgFormatter = ""; //"[ " + typeMsg + " ]";
            if (typeMsg != "")
            {
                typeMsgFormatter = "[ " + typeMsg + " ] ";
            }

            LogMessage(typeMsgFormatter + message);
            ShowMessage(message, outputFormaterParam, typeMsg);

        }

        public void ShowMessage(string message, OutputFormatterAttributes outputFormaterParam = null, string typeMsg = "")
        {
            //LogMessage(message);
            string msgFormatter = "";
            string typeMsgFormatter = "[ " + typeMsg + " ] ";
            switch (typeMsg)
            {
                case FormatterOutput.C_OK:
                    msgFormatter = _formatterOutput.OkMessage(message);
                    break;

                case FormatterOutput.C_WARING:
                    msgFormatter = _formatterOutput.WarnigMessage(message);
                    break;

                case FormatterOutput.C_ERROR:
                    msgFormatter = _formatterOutput.ErrorMessage(message);
                    break;

                case "":
                    typeMsgFormatter = "";
                    msgFormatter = _formatterOutput.CustomMessage(message, outputFormaterParam);
                    break;
            }

            _formatterOutput.ShowMessage(msgFormatter, outputFormaterParam);
        }
    }
}
