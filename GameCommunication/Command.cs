using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCommunication
{
    public delegate void ExecCommand(string command, List<string> parameters);
    public class Command
    {

        private ExecCommand _execCommand;
        private string _name;
        private string _description;
        private List<string> _lstParameters;
        private bool _enabled;


        public Command() 
        {
            _lstParameters = new List<string>();
        }

        public Command SetExecCommand(ExecCommand execCommand)
        {
            _execCommand = execCommand;
            return this;
        }

        public Command SetName(string name)
        {
            _name = name;
            return this;
        }

        public Command SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public Command SetEnabled(bool enabled)
        {
            _enabled = enabled;
            return this;
        }

        public Command SetParameters(List<string> parameters)
        {
            _lstParameters = parameters;
            return this;
        }

        public string GetName
        {
            get
            {
                return _name;
            }
        }

        public string GetDescription
        {
            get
            {
                return _description;
            }
        }

        public bool GetEnabled
        {
            get
            {
               return _enabled;
            }
        }

        public List<string> GetParameters
        {
            get
            {
                return _lstParameters;
            }
        }
        public ExecCommand GetExecCommand
        {
            get
            {
                return _execCommand;
            }
        }

        /// <summary>
        /// add parameters to the list, if the parameter exists it is replaced
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Command AddParameter(string parameter)
        {
            for (int i = 0; i<_lstParameters.Count(); i++)
            {
                if (_lstParameters[i] == parameter)
                {
                    _lstParameters[i] = parameter;
                    return this;
                }
            }

            _lstParameters.Add(parameter);
            return this;
        }

        public void ExecuteCommand(string name,List<string> parameters)
        {
            _execCommand(name, parameters);
        }

    }
}
