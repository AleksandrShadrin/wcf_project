using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfService1;
using WcfService1.Repository;


namespace Server
{
    internal class Service
    {
        bool connection_created;
        ServiceHost host;
        public Service()
        {
            connection_created = false;
        }
        public void CreateConnection()
        {
            if (connection_created == default && host == default)
            {
                var serviceAddress = "127.0.0.1:10000";
                var serviceName = "MyService";
                host = new ServiceHost(typeof(ClientService), new Uri($"net.tcp://{serviceAddress}/{serviceName}"));
                var serverBinding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IClientFeatures), serverBinding, "");
                host.Open();
                connection_created = true;
            }
        }
        public void CloseConnection()
        {
            if(host != default)
            {
                host.Close();
            }
        }

    }
}
