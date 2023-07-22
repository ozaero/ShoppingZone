using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingZone.Data.Entity
{
    public class Order:BaseEntity
    {
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }

        public string Username { get; set; }

        public string AddressName { get; set; }

        public string Address { get; set; }

        public virtual List<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine:BaseEntity
    {
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        public int ProductId { get; set; }
        public byte[] Image { get; set; }
        public virtual Product Product { get; set; }
    }
}
