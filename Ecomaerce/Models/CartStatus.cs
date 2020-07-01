using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecomaerce.Models
{
    public class CartStatus
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(500)]
        public string cartstatus { get; set; }
        public ICollection<Cart> carts { get; set; }
    }
}