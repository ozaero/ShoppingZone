using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingZone.Data.Entity
{
    public class User:BaseEntity
    {
        [Required(ErrorMessage = "Please enter your user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        public string Role { get; set; }

        public byte[] ProfileImage { get; set; }

        public DateTime JoinTime { get; set; }
    }
}
