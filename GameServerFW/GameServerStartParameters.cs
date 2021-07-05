/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW
{
    public class GameServerStartParameters
    {

        /// <summary>
        /// no creo que esta clase haga falta, de hecho, es al pedo
        /// </summary>

        private class Parameters
        {
            internal string parameter;
            internal string description;
        }

        private List<Parameters> _lstParameters;

        public GameServerStartParameters()
        {
            _lstParameters = new List<Parameters>();
        }

        public bool SetParameters(string parameter,string description)
        {
            bool result = true;
            char[] charsToTrim = { '*', ' ', '\'' };
            parameter = parameter.Trim(charsToTrim);
            description = description.Trim(charsToTrim);

            if (parameter == "")
            {
                result = false;
                throw new Exception("parameter cannot be empty");
            }
            if (description == "")
            {
                result = false;
                throw new Exception("description cannot be empty");
            }

            //buscar el parametro
            if (FindParameterIndex(parameter)>=0)
            {
                result = false;
                //the PP parameter already exists
                throw new Exception($"parameter {parameter} already exists");
            }


            if (result)
            {
                Parameters aux = new Parameters();
                aux.parameter = parameter;
                aux.description = description;

                _lstParameters.Add(aux);
            }


            return result;
        }

        public List<string> GetParameterList()
        {
            List<string> lst = new List<string>();
            foreach(var parameter in _lstParameters)
            {
                string param = parameter.parameter;
                lst.Add(param);
            }

            return lst;
        }

        public List<string> GetDescriptionList()
        {
            List<string> lst = new List<string>();
            foreach (var parameter in _lstParameters)
            {
                string desc = parameter.description;
                lst.Add(desc);
            }

            return lst;
        }

        public string GetParameter(int index)
        {
            return "";
        }

        public string GetParameter(string parameter)
        {

            return "";
        }

        public int Count()
        {
            return _lstParameters.Count();
        }

        private int FindParameterIndex(string parameter)
        {
            int index = -1;

            for (int i = 0; i<_lstParameters.Count();i++)
            {
                if (_lstParameters[i].parameter == parameter)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        
    }
}
*/