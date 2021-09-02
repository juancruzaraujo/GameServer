using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GameServerFW.EventParameters;

namespace GameServerFW
{
    public class LoggerManager
    {
        private List<string> lstLog;
        private char directorySeparatorChar = Path.DirectorySeparatorChar;
        private string _logFile;
        private StreamWriter _sw;
        private static bool _logFileCreated= false;

        public delegate void Delegate_Log_Event(LoggerManagerEventsParameters loggerManagerEventsParameters);
        public event Delegate_Log_Event Event_Log;
        public void LoggerEvent(LoggerManagerEventsParameters loggerManagerEventsParameters)
        {
            this.Event_Log(loggerManagerEventsParameters);
        }

        public LoggerManager()
        {
            
        }

        /// <summary>
        /// create txt logfile in current directory
        /// </summary>
        /// <param name="logName">file log name</param>
        /// <param name="path">path</param>
        public void CreateLogFile(string logName, string path)
        {
            lstLog = new List<string>();
            //logFile = Directory.GetCurrentDirectory() + directorySeparatorChar + logName + ".txt";
            _logFile = path + Path.DirectorySeparatorChar + logName;
            CreateOpenLogFile();
        }

        private string Log(string logMessage)
        {
            string message = "[" + DateTime.Now + "]" + logMessage;
            lstLog.Add(message);
            return message;

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

                    LoggerManagerEventsParameters loggerManagerEventsParameters = new LoggerManagerEventsParameters();
                    loggerManagerEventsParameters.SetEventType(LoggerManagerEventsParameters.LoggerManagerEventType.CREATE_OR_APPEND_LOG_FILE_OK);
                    LoggerEvent(loggerManagerEventsParameters);
                }

            }
            catch(Exception err)
            {
                LoggerManagerEventsParameters loggerManagerEventsParameters = new LoggerManagerEventsParameters();
                loggerManagerEventsParameters.SetEventType(LoggerManagerEventsParameters.LoggerManagerEventType.CREATE_OR_APPEND_LOG_FILE_ERROR)
                    .SetEventMessage(err.Message);
                LoggerEvent(loggerManagerEventsParameters);
            }
        }

        /// <summary>
        /// write a line and save the file. use with care because it can slow down the execution if used for many lines in a row
        /// </summary>
        /// <param name="message"></param>
        public string WriteAndSaveLog(string message)
        {
            try
            {
                WriteLog(message);
                SaveLog();
                CreateOpenLogFile();
            }
            catch(Exception err)
            {
                LoggerManagerEventsParameters loggerManagerEventsParameters = new LoggerManagerEventsParameters();
                loggerManagerEventsParameters.SetEventType(LoggerManagerEventsParameters.LoggerManagerEventType.SAVE_LOG_ERROR)
                    .SetEventMessage(err.Message);

                LoggerEvent(loggerManagerEventsParameters);
            }
            
            
            return message;
            
        }

        /// <summary>
        /// write a line
        /// </summary>
        /// <param name="message"></param>
        public string WriteLog(string message)
        {
            try
            {
                if (_logFileCreated)
                {
                    _sw.WriteLine(Log(message));
                }
                return message;
            }
            catch(Exception err)
            {

                LoggerManagerEventsParameters loggerManagerEventsParameters = new LoggerManagerEventsParameters();
                loggerManagerEventsParameters.SetEventType(LoggerManagerEventsParameters.LoggerManagerEventType.WRITE_LOG_FILE_ERROR)
                    .SetEventMessage(err.Message);

                LoggerEvent(loggerManagerEventsParameters);
                return null;
            }
        }

        /// <summary>
        /// save log file
        /// </summary>
        public void SaveLog()
        {
            if (_logFileCreated)
            {
                _sw.Close();
            }
        }

        /// <summary>
        /// return list all logs
        /// </summary>
        /// <returns></returns>
        public List<string> getLogBuffer()
        {
            return lstLog;
        }

        /// <summary>
        /// clear log list
        /// </summary>
        public void ClearLogBuffer()
        {
            lstLog.Clear();
        }
        
    }
}
