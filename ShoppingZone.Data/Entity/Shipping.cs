using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingZone.Data.Entity
{
    public class Shipping
    {
        public string Username { get; set; }

        [Required(ErrorMessage ="Please enter the address name")]
        public string AddressName { get; set; }

        [Required(ErrorMessage = "Please enter the address")]
        public string Address { get; set; }
    }
}
