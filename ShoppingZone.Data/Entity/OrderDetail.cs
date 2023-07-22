using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingZone.Data.Entity
{
    public class OrderDetail:BaseEntity
    {
        public string OrderNumber { get; set; }

        public double Total { get; set; }

        public DateTime OrderDate { get; set; }

        public string Username { get; set; }

        public string AddressName { get; set; }

        public string Address { get; set; }

        public virtual List<OrderLineDetail> OrderLines { get; set; }
    }

    public class OrderLineDetail:BaseEntity
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public byte[] Image { get; set; }
    }
}
