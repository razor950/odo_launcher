using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ODO_Launcher
{
    class Config
    {
        static readonly string configURL = Properties.Settings.Default.Config_URL;
        static XElement configDocument;

        public static void First()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public static void LoadConfig()
        {
            try
            {
                configDocument = XElement.Load(configURL);
                XNamespace ns = configDocument.GetDefaultNamespace();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR!");
            }
        }

        public static bool GetBool(string name)
        {
            try
            {
                var result = configDocument.Element(name).Value;
                var boolResult = Convert.ToBoolean(result);

                return boolResult;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR!");
            }

            return false;
        }

        public static string GetString(string name)
        {
            try
            {
                var result = configDocument.Element(name).Value;
                var stringResult = Convert.ToString(result);

                return stringResult;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR!");
            }

            return null;
        }

        public static int GetInt(string name)
        {
            try
            {
                var result = configDocument.Element(name).Value;
                var intResult = Convert.ToInt32(result);

                return intResult;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR!");
            }

            return 0;
        }

    }
}
