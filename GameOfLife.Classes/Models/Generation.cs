using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    public class Generation
    {
        public bool[,] Field { get; set; }

        public Generation(int numberOfColumns, int numberOfRows)
        {
            Field = new bool[numberOfColumns, numberOfRows];
        }

        public List<Tuple<int, int>> Evolve()
        {
            /// <summary>
            /// Main algorithm
            /// </summary>
            List<Tuple<int, int>> listOfCoorinates = new List<Tuple<int, int>>(); // Fill the coordinates to change
            return listOfCoorinates;
        }
    }
}
