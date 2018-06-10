using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    public class Pattern
    {
        public string Name { get; set; }
        public List<Tuple<int,int>> LivingCells { get; set; }
        public int FieldXOffset { get; set; }
        public int FieldYOffset { get; set; }
        public bool Fits { get; set; }
    }
}
