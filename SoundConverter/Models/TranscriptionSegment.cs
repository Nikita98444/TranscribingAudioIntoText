using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConverter.Models
{
    /// <summary>
    /// Класс для десериализации сегментов транскрипции и диаризации
    /// </summary>
    public class TranscriptionSegment
    {
        public string speaker { get; set; }
        public double start { get; set; }
        public double end { get; set; }
        public string text { get; set; }
    }
}
