using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingZone.Data.Entity
{
    public class OrderView:BaseEntity
    {
        public string OrderNumber { get; set; }

        public double Total { get; set; }

        public DateTime OrderDate { get; set; }

        public string Username { get; set; }

        public string AddressName { get; set; }

        public string Address { get; set; }
    }
}
