using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    public class Repository
    {
        public List<Pattern> Patterns { get; set; } = new List<Pattern>();

        public Repository()
        {
            foreach (var path in Directory.GetFiles("../../../GameOfLife.Classes/Patterns"))
            {
                var pattern = RLEDecoder.PatternFits(patternFilePath: path);
                if (pattern.Fits)
                {
                    Patterns.Add(RLEDecoder.DecodePattern(path, pattern: pattern));
                }
            }
        }
    }
}
