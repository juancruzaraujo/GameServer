using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleOutputFormater;

namespace ShowAndLogMessage
{
    /// <summary>
    /// use LoggerMessage.GetInstance()
    /// </summary>
    public class LogInfo
    {
        #if DEBUG
            private static bool debug_Mode = true;
        #else
            private /*static*/ bool debug_Mode = false;
        #endif

        const string C_OK = " OK ";
        const string C_ERROR = " ERROR ";
        const string C_WARING = " WARNIG ";
        const string C_NO_TYPE = "";

        private List<string> _lstMessages;
        private string _logFile;
        private StreamWriter _sw;
        private static bool _logFileCreated = false;
        private int _maxLinesToSaveLogFile;
        private int _countLines;

        public int MaxLinesToSaveLogFile
        {
            get
            {
                return _maxLinesToSaveLogFile;
            }
            set
            {
                _maxLinesToSaveLogFile = value;
            }
        }

        public enum typeMsg
        {
            OK,
            ERROR,
            WARNING,
            NO_TYPE
        }

        private FormatterOutput _formatterOutput;
        static LogInfo _loggerMessageInstance;        
        
        public bool LogFileCreated
        {
            get
            {
                return _logFileCreated;
            }
        }

        private LogInfo()
        {
            _lstMessages = new List<string>();   
            _formatterOutput = FormatterOutput.GetInstance();
        }
        
        public static LogInfo GetInstance()
        {
            if (_loggerMessageInstance == null)
            {
                _loggerMessageInstance = new LogInfo();
            }

            return _loggerMessageInstance;
        }

        public void CreateLogFile(string logName, string path)
        {
            try
            {
                _logFile = path + Path.DirectorySeparatorChar + logName;
                CreateOpenLogFile();
            }
            catch(Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        private void CreateOpenLogFile()
        {
            try
            {
                

                if (!File.Exists(_logFile))
                {
                    _sw = File.CreateText(_logFile);
                }
                else
                {
                    _sw = File.AppendText(_logFile);
                }

                //lo ejecutamos una sola vez, si guardo el archivo lo tengo que volver a abrir
                //y eso puede hacer que salte un error de stack
                if (!_logFileCreated)
                {
                    _logFileCreated = true;
                }

            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public void LogMessage(string message)
        {
            try
            {
                _lstMessages.Add(PutDateTime(message));
                if (_countLines < _maxLinesToSaveLogFile)
                {
                    _countLines++;
                }
                else
                {
                    if (_sw != null)
                    {
                        for (int i = 0; i < _lstMessages.Count(); i++)
                        {
                            _sw.WriteLine(_lstMessages[i]);
                        }
                        _sw.Flush();
                        _sw.Close();
                        CreateOpenLogFile();
                        _countLines = 0;
                        _lstMessages.Clear();
                    }
                    
                }
            }
            catch(Exception err)
            {
                ShowMessage(message);
                throw new Exception(err.Message);
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
        /// <summary>
        /// displays a message in the terminal. 
        /// If the "outputFormaterParam" parameter is used this applies from the beginning of the string until the break character is found. 
        /// if you want different formats, you must use a string previously formatted with the "FormattText" function
        /// </summary>
        /// <param name="message">string to show</param>
        /// <param name="outputFormaterParam">attributes to apply in the chain. default is null</param>
        /// <param name="msgType">message type, "OK", "WARNIG", "ERROR" and "NO_TYPE", the default value is NO_TYPE. 
        /// if a different type is chosen, the function shows in the terminal "[TYPE] message</param>
        public void ShowMessage(string message, OutputFormatterAttributes outputFormaterParam = null, typeMsg msgType = typeMsg.NO_TYPE)
        {
            string msgFormatter = "";
            string typeMsgStr = GetMsgTypeSTR(msgType);

            if (typeMsgStr == C_OK)
            {
                msgFormatter = _formatterOutput.OkMessage() + FormattText(message, outputFormaterParam);
            }
            else if (typeMsgStr == C_WARING)
            {
                msgFormatter = _formatterOutput.WarnigMessage() + FormattText(message, outputFormaterParam);
            }
            else if (typeMsgStr == C_ERROR)
            {
                msgFormatter = _formatterOutput.ErrorMessage() + FormattText(message, outputFormaterParam);

            }
            else if (typeMsgStr == C_NO_TYPE)
            {
                msgFormatter = _formatterOutput.GetForrmatedText(message, outputFormaterParam);
            }

            _formatterOutput.ShowMessage(msgFormatter);
        }

        public string FormattText(string message, OutputFormatterAttributes outputFormaterParam)
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

        private string PutDateTime(string logMessage)
        {
            string message = "[" + DateTime.Now + "] " + logMessage;

            return message;

        }

    }
}
