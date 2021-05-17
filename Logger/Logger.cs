using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Logger
{
    public class Logger
    {
        private List<string> lstLog;
        private char directorySeparatorChar = Path.DirectorySeparatorChar;
        private string logFile;
        private StreamWriter sw;


        public delegate void Delegate_Log_Event(string eventMessage);
        public event Delegate_Log_Event Event_Log;
        public void LoggerEvent(string eventMessage)
        {
            this.Event_Log(eventMessage);
        }

        /// <summary>
        /// create txt logfile in current directory
        /// </summary>
        /// <param name="logName">file log name</param>
        /// <param name="path">path</param>
        public Logger(string logName, string path)
        {
            lstLog = new List<string>();
            //logFile = Directory.GetCurrentDirectory() + directorySeparatorChar + logName + ".txt";
            logFile = path + Path.DirectorySeparatorChar + logName;
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
                if (!File.Exists(logFile))
                {
                    sw = File.CreateText(logFile);
                }
                else
                {
                    sw = File.AppendText(logFile);
                }
                
            }
            catch(Exception err)
            {
                LoggerEvent(err.Message);
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
            }
            catch(Exception err)
            {
                LoggerEvent(err.Message);
            }
            
            
            return message;
            
        }

        /// <summary>
        /// write a line
        /// </summary>
        /// <param name="message"></param>
        public string WriteLog(string message)
        {
            sw.WriteLine(Log(message));
            return message;
        }

        /// <summary>
        /// save log file
        /// </summary>
        public void SaveLog()
        {
            sw.Close();
        }

        /// <summary>
        /// return list all logs
        /// </summary>
        /// <returns></returns>
        public List<string> getLog()
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
