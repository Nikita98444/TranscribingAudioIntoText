using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConverter.Models
{
    public class DiarizationSegment
    {
        public double start { get; set; }
        public double end { get; set; }
        public string speaker { get; set; }
    }
}
