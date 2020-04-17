using Microsoft.AspNetCore.Http;
using StoreAssignment.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAssignment.VModel
{
    public class VProduct
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Item Name:")]
        public string ProductName { get; set; }
        [Display(Name ="Title:")]
        public string Title { get; set; }
        [Required]
        [Display(Name ="Brand:")]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        [Required]
        [Display(Name ="Category:")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required]
        [Display(Name ="SubCategory:")]
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        [Required]
        [Display(Name ="Availability Date:")]
        public DateTime AvailabilityDate { get; set; }
        [Required]
        [Display(Name ="IsAvailable:")]
        public bool IsAvailable { get; set; }
        [Display(Name ="Upload Image:")]
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxImageSize(FileSize = 500*1024, ErrorMessage = "Maximum allowed file size is 500 KB")]
        [ImageExtensions(Extensions = "png,jpeg,JPEG,PNG", ErrorMessage = "Please select only Supported Files .png | .jpeg")]
        public IFormFile Image { get; set; }
        public byte[] ImageData { get; set; }
        [Required]
        [Display(Name ="Description:")]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
