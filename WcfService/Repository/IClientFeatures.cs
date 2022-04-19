using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfService1.Models;

namespace WcfService1.Repository
{
    [ServiceContract]
    public interface IClientFeatures
    {
        [OperationContract]
        IEnumerable<Contract> GetAllContracts();
    }
}
