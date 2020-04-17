using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAssignment.Models
{
    public class SubCategory
    {
        [Key]
        [Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CategoryFK")]
        public int CategoryId { get; set; }
        public virtual Category CategoryFK { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
