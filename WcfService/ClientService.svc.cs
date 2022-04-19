using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.Models;
using WcfService1.Repository;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientService.svc or ClientService.svc.cs at the Solution Explorer and start debugging.
    public class ClientService : IClientFeatures
    {
        IClientFeatures repo;
        ClientService()
        {
            repo = new ContractsRepository();
        }
        //public  void AddContract(Contract contract)
        //{
        //    repo.AddContract(contract);
        //}

        //public  void DeleteContractById(int id)
        //{
        //    repo.DeleteContractById(id);
        //}

        //public  void DeleteContractByNumber(int number)
        //{
        //    repo.DeleteContractByNumber(number);
        //}

        public IEnumerable<Contract> GetAllContracts()
        {
            return repo.GetAllContracts();
        }

        //public  void UpdateContract(Contract changedContract)
        //{
        //    repo.UpdateContract(changedContract);
        //}
    }
}
