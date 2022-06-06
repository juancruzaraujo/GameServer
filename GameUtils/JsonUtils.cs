
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;


namespace GameUtils
{
    public class JsonUtils
    {
        string _errorMessage;

        /// <summary>
        /// Show the error message if it exists and clear the cache
        /// </summary>
        public string GetErrorMessage
        {
            get
            {
                string aux = _errorMessage;
                _errorMessage = "";
                return aux;
            }
        }


        public string ClassToJson(Type theClass, object obj)
        {
            try
            {
                var stream1 = new MemoryStream();
                var ser = new DataContractJsonSerializer(theClass);
                ser.WriteObject(stream1, obj);

                stream1.Position = 0;
                var sr = new StreamReader(stream1);

                string json = sr.ReadToEnd();
                return json;
            }
            catch(Exception err)
            {
                _errorMessage = err.Message;
                return "";
            }
        }

        public bool JsonToFile(string path,Type theClass,object obj)
        {
            bool result = false;

            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(theClass);
                MemoryStream msObj = new MemoryStream();
                // Escribe los datos del formato Json serializado en la secuencia
                js.WriteObject(msObj, obj);
                msObj.Position = 0;
                // Comienza a leer datos en la secuencia desde 0
                StreamReader sr = new StreamReader(msObj, Encoding.UTF8);
                string json = sr.ReadToEnd();
                sr.Close();
                msObj.Close();

                File.WriteAllText(path, json);


                result = true;
                return result;
            }
            catch(Exception err)
            {
                _errorMessage = err.Message;
                return result;
            }
           
        }
    }
}
