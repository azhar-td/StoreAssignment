using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace StoreAssignment.Models
{
    public class Product
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Title { get; set; }
        [Required]
        [ForeignKey("Brand")]
        //[Range(1, int.MaxValue)]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        [Required]
        public DateTime AvailabilityDate { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
