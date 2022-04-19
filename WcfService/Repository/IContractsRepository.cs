using WcfService1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1.Repository
{
    [ServiceContract]
    public interface IContractsRepository: IServerFeatures
    {
    }
}
