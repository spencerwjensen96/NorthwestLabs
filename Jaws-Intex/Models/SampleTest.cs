using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Sample_Test")]
    public class SampleTest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Sample")]
        [Required(ErrorMessage = "Sample is required")]
        public int SampleId { get; set; }
        public virtual Sample Sample { get; set; }

        [ForeignKey("Test")]
        [Required(ErrorMessage = "Test is required")]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }

    }
}