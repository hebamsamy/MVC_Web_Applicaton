using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomaerce.Models
{
    public class Cart
    {
        public int ID { get; set; }
        [Required]
        [ForeignKey("product")]
        public int ProductID { get; set; }
        public Product product { get; set; }
        //[Required]
        //[ForeignKey("member")]
        //public int MemberID { get; set; }
        //public Member member { get; set; }
        [Required]
        [ForeignKey("cartStatus")]
        public int CartstatusID { get; set; }
        public CartStatus cartStatus { get; set; }
        public int count { get; set; }
    }
}