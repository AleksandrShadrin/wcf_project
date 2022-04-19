using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1.Models
{
    public class Contract {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
