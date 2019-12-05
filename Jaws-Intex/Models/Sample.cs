using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Sample")]
    public class Sample
    {
        [Key]
        public int SampleId { get; set; }

        [ForeignKey("Compound")]
        [Display(Name = "Compound ID")]
        [Required]
        public int CompoundId { get; set; }

        public virtual Compound Compound { get; set; }

        [Display(Name = "Sequence")]
        [Required]
        public int Sequence { get; set; }

        [Display(Name = "Concentration (mg/mL)")]
        [Required]
        public string Concentration { get; set; }

        [Display(Name = "Absorption")]
        public decimal? Absorption { get; set; }

        [Display(Name = "Quantity (mg)")]
        public string Quantity { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Sample Results URL")]
        public string Sample_File_URL { get; set; }

        public ICollection<SampleTest> SampleTests { get; set; }

    }
}