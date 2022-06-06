using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GameServerFW.EventParameters;
using ShowAndLogMessage;
using ConsoleOutputFormater;
using static ShowAndLogMessage.LogInfo;

namespace GameServerFW
{
    public class LoggerManager
    {
        private List<string> lstLog;
        private char directorySeparatorChar = Path.DirectorySeparatorChar;
        private string _logFile;
        //private StreamWriter _sw;
        //private static bool _logFileCreated= false;

        LogInfo _logInfo;
        private OutputFormatterAttributes _outputFormatterAttributes;
        

        
        public OutputFormatterAttributes OutputFormatterAttributes
        {
            get
            {
                return _outputFormatterAttributes;
            }
            set
            {
                _outputFormatterAttributes = value;
            }
        }
        

        public delegate void Delegate_Log_Event(LoggerManagerEventsParameters loggerManagerEventsParameters);
        public event Delegate_Log_Event Event_Log;
        public void LoggerEvent(LoggerManagerEventsParameters loggerManagerEventsParameters)
        {
            this.Event_Log(loggerManagerEventsParameters);
        }

        internal LoggerManager()
        {
            _logInfo = LogInfo.GetInstance();
        }

        /// <summary>
        /// create txt logfile in current directory
        /// </summary>
        /// <param name="logName">file log name</param>
        /// <param name="path">path</param>
        public void CreateLogFile(string logName, string path)
        {
            lstLog = new List<string>();
            
            _logFile = path + Path.DirectorySeparatorChar + logName;

            _logInfo.CreateLogFile(logName, path);
        }

        /// <summary>
        /// return list all logs
        /// </summary>
        /// <returns></returns>
        internal List<string> getLogBuffer()
        {
            return lstLog;
        }

        /// <summary>
        /// clear log list
        /// </summary>
        internal void ClearLogBuffer()
        {
            lstLog.Clear();
        }
        
        public void MaxLogLinesToSave(int maxLines)
        {
            _logInfo.MaxLinesToSaveLogFile = maxLines;
        }


        public void ShowMessage(string messaje)
        {
            ShowMessage(messaje);
        }

        public void ShowMessage(string message, OutputFormatterAttributes outputFormaterParam = null, typeMsg msgType = typeMsg.NO_TYPE)
        {
            try
            {
                _logInfo.ShowMessage(message, outputFormaterParam, msgType);
                
            }
            catch(Exception err)
            {
                LoggerManagerEventsParameters loggerManagerEventsParameters = new LoggerManagerEventsParameters();
                loggerManagerEventsParameters.SetEventType(LoggerManagerEventsParameters.LoggerManagerEventType.SHOW_LOG_ERROR)
                    .SetEventMessage(err.Message);

                LoggerEvent(loggerManagerEventsParameters);
            }
        }

        public void ShowAndLogMessage(string message)
        {
            ShowAndLogMessage(message,null);
        }

        public void ShowAndLogMessage(string messaje, OutputFormatterAttributes outputFormaterParam = null, typeMsg msgType = typeMsg.NO_TYPE)
        {
            try
            {
                _logInfo.ShowAndLogMessage(messaje, outputFormaterParam, msgType);
            }
            catch(Exception err)
            {
                LoggerManagerEventsParameters loggerManagerEventsParameters = new LoggerManagerEventsParameters();
                loggerManagerEventsParameters.SetEventType(LoggerManagerEventsParameters.LoggerManagerEventType.WRITE_LOG_FILE_ERROR)
                    .SetEventMessage(err.Message);

                LoggerEvent(loggerManagerEventsParameters);
            }
        }

    }
}
