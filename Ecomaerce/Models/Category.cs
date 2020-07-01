using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomaerce.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product> products { get; set; }
    }
}