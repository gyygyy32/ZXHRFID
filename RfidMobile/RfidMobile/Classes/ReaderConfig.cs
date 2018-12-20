using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using RfidMobile.API;
using System.Reflection;

namespace RfidMobile.Classes
{
    class ReaderConfig
    {
        static string configFilePath = Assembly.GetExecutingAssembly().GetName().CodeBase.Substring(0, Assembly.GetExecutingAssembly().GetName().CodeBase.LastIndexOf('\\') + 1) + "settings.xml";
        static XDocument doc = XDocument.Load(configFilePath);

        public static IDevice _rfidReader = null;

        public static Dictionary<string, RFIDConstants> _dic_rfidConstants = new Dictionary<string, RFIDConstants>();

        public static string BuildUrl()
        {
            return string.Format("http://{0}:{1}/RFID.svc", doc.Root.Element("Host").Value, doc.Root.Element("Port").Value);
        }

        public static void SaveConfig(string hostValue, string portValue)
        {
            doc.Root.SetElementValue("Host", hostValue);

            doc.Root.SetElementValue("Port", portValue);

            doc.Save(configFilePath);
        }

        public static string GetXmlNodeValue(string nodeName)
        {
            return doc.Root.Element(nodeName).Value;
        }

        /*
         *  initialise the reader by setting device type
         */
        public static void InitRfidReader()
        {
            try
            {
                GetRfidConstantsFromXML();

                string deviceType = doc.Root.Element("Device").Value;

                if (deviceType=="D3000")
                {
                    _rfidReader = new D3000();
                }

                _rfidReader.Open();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private static void GetRfidConstantsFromXML()
        {
            _dic_rfidConstants.Clear();

            foreach (XElement element in doc.Element("Root").Elements("Customer"))
            {
                string mfg_country = element.Element("mfg_country").Value;
                string mfg_name = element.Element("mfg_name").Value;
                string iec_date = element.Element("iec_date").Value;
                string iec_verfy = element.Element("iec_verfy").Value;
                string iso = element.Element("iso").Value;
                string producttype = element.Element("producttype").Value;

                RFIDConstants rfidConstant = new RFIDConstants(mfg_country, mfg_name, iec_date, iec_verfy, iso, producttype);

                _dic_rfidConstants.Add(element.Attribute("id").Value, rfidConstant);
            }
        }

        public static void ReleaseReader()
        {
            try
            {
                if (_rfidReader != null)
                {
                     _rfidReader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
