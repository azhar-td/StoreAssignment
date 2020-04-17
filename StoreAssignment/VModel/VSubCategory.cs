using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAssignment.VModel
{
    public class VSubCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Name:")]
        public string Name { get; set; }
    }
}
