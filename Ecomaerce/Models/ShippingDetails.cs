using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomaerce.Models
{
    public class ShippingDetails
    {
        public int ID { get; set; }
        [Required]
        [ForeignKey("member")]

        public int MemberID { get; set; }
        public Member member { get; set; }
        [MaxLength(200)]
        [Required]
        public string Adress { get; set; }
        [MaxLength(200)]
        [Required]
        public string City { get; set; }
        [MaxLength(200)]
        [Required]

        public string State { get; set; }
        [MaxLength(200)]
        [Required]

        public string Country { get; set; }
        [MaxLength(50)]
        [Required]

        public string ZipCode { get; set; }
        public int OrderID { get; set; }
        public Decimal AmountPaid { get; set; }
        [MaxLength(50)]
        [Required]

        public string PaymentType { get; set; }

    }
}