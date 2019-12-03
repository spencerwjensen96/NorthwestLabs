using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    public class Sample
    {
        public int SampleId { get; set; }

        [Display(Name = "Compound ID")]
        [Required]
        public int CompoundId { get; set; }

        [Display(Name = "Sequence")]
        [Required]
        public int Sequence { get; set; }

        [Display(Name = "Test ID")]
        [Required]
        public int TestId { get; set; }

        [Display(Name = "Concentration")]
        [Required]
        public string Concentration { get; set; }

        [Display(Name = "Absorption")]
        public string Absorption { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Display(Name = "Sample File URL")]
        public string Sample_File_URL { get; set; }

    }
}