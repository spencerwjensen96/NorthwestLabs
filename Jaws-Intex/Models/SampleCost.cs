using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    public class SampleCost
    {
        public int SampleId { get; set; }
        public decimal Cost { get; set; }
        public int LT { get; set; }
        public int Sequence { get; set; }
    }
}