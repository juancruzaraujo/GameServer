using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCommunication
{
    
    public class CommandsManager
    {
        private const string C_ERROR_MESSAGE_EXISTING_COMMAND = "existing command";
        private const string C_ERROR_MESSAGE_COMMAND_NOT_FOUND = "command not found";
        private const string C_ERROR_MESSAGE_COMMAND_DISABLED = "command disabled";

        private List<Command> _lstCommands;

        string _message;

        public string GetErrorMessage
        {
            get
            {
                string aux = _message;
                _message = "";
                return aux;
            }
        }

        public CommandsManager()
        {
            _lstCommands = new List<Command>();
        }

        public void AddCommand(Command command)
        {
            foreach(Command com in _lstCommands)
            {
                if (com.GetExecCommand == command.GetExecCommand)
                {
                    _message = C_ERROR_MESSAGE_EXISTING_COMMAND;
                }
            }

            _lstCommands.Add(command);
        }

        public void EnabledCommand(string name, bool enabled)
        {
            int index = GetCommandIndex(name);
            if (index == -1)
            {
                _message = C_ERROR_MESSAGE_COMMAND_NOT_FOUND;
            }

            _lstCommands[index].SetEnabled(enabled);
        }

        public void DeleteCommand(string name)
        {
            int index = GetCommandIndex(name);
            if (index == -1)
            {
                _message = C_ERROR_MESSAGE_COMMAND_NOT_FOUND;
            }

            _lstCommands.RemoveAt(index);
        }

        public void ExecuteCommand(string name,List<string> parameters)
        {
            int index = GetCommandIndex(name);
            if (index == -1)
            {
                _message = C_ERROR_MESSAGE_COMMAND_NOT_FOUND;
                return;
            }

            if (_lstCommands[index].GetEnabled)
            {
                _lstCommands[index].GetExecCommand(name, parameters);
            }
            else
            {
                _message = C_ERROR_MESSAGE_COMMAND_DISABLED;
                return;
            }

        }

        private int GetCommandIndex(string name)
        {
            for (int i = 0; i < _lstCommands.Count();i++)
            {
                if (_lstCommands[i].GetName == name)
                {
                    return i;
                }
            }
            _message = C_ERROR_MESSAGE_COMMAND_NOT_FOUND;
            return -1;
        }
       

    }
}
