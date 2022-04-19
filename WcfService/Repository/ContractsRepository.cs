using WcfService1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ServiceModel;

namespace WcfService1.Repository
{
    public class ContractsRepository :  IContractsRepository
    {
        private ContractsDbContext _context;
        public ContractsRepository()
        {
            _context = new ContractsDbContext();
        }
        public IEnumerable<Contract> GetAllContracts()
        {
            return _context.Contracts.ToList();
        }
        public void AddContract(Contract contract)
        {
            if(contract == default)
            {
                return;
            }
            _context.Contracts.Add(contract);
            _context.SaveChanges();
        }
        public void DeleteContractById(int id)
        {
            Contract contract = _context.Contracts.Where(x => x.Id == id).FirstOrDefault();
            if(contract == default)
            {
                return;
            }
            _context.Contracts.Remove(contract);
            _context.SaveChanges();
        }

        public void DeleteContractByNumber(int number)
        {
            Contract contract = _context.Contracts.Where(x => x.Number == number).FirstOrDefault();
            if (contract == default)
            {
                return;
            }
            _context.Contracts.Remove(contract);
            _context.SaveChanges();
        }
        public void UpdateContract(Contract changedContract)
        {
            Contract oldContract = _context.Contracts.Where(x => x.Id == changedContract.Id).FirstOrDefault();
            if(oldContract == default)
            {
                return;
            }
            oldContract.Number = changedContract.Number;
            oldContract.LastUpdate = changedContract.LastUpdate;
            oldContract.Date = changedContract.Date;
            _context.SaveChanges();
        }
    }
}
