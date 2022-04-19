using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfService1.Repository;

namespace Client
{
    internal class ServerConnection
    {
        bool _connected;
        IClientFeatures _service;
        ChannelFactory<IClientFeatures> _factory;
        public ServerConnection()
        {
            _connected = false;
        }
        public IClientFeatures CreateConnection()
        {
            if(_service == default)
            {
                var serviceAddress = "127.0.0.1:10000";
                var serviceName = "MyService";

                Uri tcpUri = new Uri($"net.tcp://{serviceAddress}/{serviceName}");
                EndpointAddress address = new EndpointAddress(tcpUri);
                NetTcpBinding clientBinding = new NetTcpBinding();
                _factory = new ChannelFactory<IClientFeatures>(clientBinding, address);
                _service = _factory.CreateChannel();
                _connected = true;
                return _service;
            }
            return _service;
        }
        public void Disconnection()
        {
            if(_factory != default)
            {
                _factory.Close();
                _service = default;
                _connected = false;
            }
        }

    }
}
