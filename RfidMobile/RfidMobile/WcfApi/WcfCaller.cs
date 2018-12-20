using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using RfidMobile.Classes;

namespace RfidMobile
{
    class WcfCaller
    {
        public static void GetModuleInfo(Action<ModuleInfo, Exception> action, string[] prms)
        {
            RFIDServiceClient client = null;

            try
            {
                string remoteAddress = ReaderConfig.BuildUrl();
                EndpointAddress endpoint = new EndpointAddress(remoteAddress);
                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                //binding.Security = BasicHttpSecurityMode.Transport;
                binding.OpenTimeout = new TimeSpan(0, 0, 10);
                binding.CloseTimeout = new TimeSpan(0, 0, 10);
                binding.SendTimeout = new TimeSpan(0, 0, 10);
                binding.ReceiveTimeout = new TimeSpan(0, 0, 10);
                binding.MaxReceivedMessageSize = int.MaxValue;
                binding.MaxBufferPoolSize = int.MaxValue;
                binding.MaxBufferSize = int.MaxValue;
                client = new RFIDServiceClient(binding, endpoint);

                //client = new RFIDServiceClient();

                action(client.getModuleInfo(prms), null);

                //client.Close();

            }
           catch (Exception ex)
            {
                //client.Abort();
                action(null, ex);
            }
            //finally 
            //{

            //    ICommunicationObject comObj = ((ICommunicationObject)client);

              

            //    if (comObj.State == CommunicationState.Faulted)
            //    {
            //        comObj.Abort();
            //    }
            //    else
            //    {
            //        comObj.Close();
            //    }
            //}

        }

        public static void WriteLog(Action<Exception> action, string[] prms)
        {
            RFIDServiceClient client = null;

            try
            {
                string remoteAddress = ReaderConfig.BuildUrl();
                EndpointAddress endpoint = new EndpointAddress(remoteAddress);
                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                //binding.Security = BasicHttpSecurityMode.Transport;
                binding.OpenTimeout = new TimeSpan(0, 0, 10);
                binding.CloseTimeout = new TimeSpan(0, 0, 10);
                binding.SendTimeout = new TimeSpan(0, 0, 10);
                binding.ReceiveTimeout = new TimeSpan(0, 0, 10);
                binding.MaxReceivedMessageSize = int.MaxValue;
                binding.MaxBufferPoolSize = int.MaxValue;
                binding.MaxBufferSize = int.MaxValue;
                client = new RFIDServiceClient(binding, endpoint);

                client.WriteLog(prms);
                action(null);

            }
            catch (Exception e)
            {
                action(e);
            }
        }
    }
}
