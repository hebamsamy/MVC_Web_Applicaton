using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomaerce.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Category Name Required")]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage = "minimum 3 characters")]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [ForeignKey("category")]
        [Range(1, 50)]
        public int CategoryID { get; set; }
        public Category category { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ModifiedDate { get; set; }
        [Required(ErrorMessage = "Category Description Required")]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Category Image Required")]
        [MaxLength(500)]
        public string Image { get; set; }
        public bool IsFeatured { get; set; }
        [Required(ErrorMessage = "Category Quantity Required")]
        [Range(typeof(int), "1", "100", ErrorMessage = "Invalid Quantity")]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "money")]
        [Range(1, 5000, ErrorMessage = "Invalid Price")]
        public Decimal Price { get; set; }
        public ICollection<Cart> carts { get; set; }
    }
}