using GameOfLife.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    public class Generation
    {
        public Location[,] Reachability { get; set; }

        public int[,] Field { get; set; }

        public Generation(int numberOfColumns, int numberOfRows)
        {
            Field = new int[numberOfColumns, numberOfRows];
            Reachability = new Location[numberOfColumns, numberOfRows];
        }

        public List<Tuple<int, int>> Evolve()
        {
            /// <summary>
            /// Main algorithm
            /// </summary>
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<Tuple<int, int>> coordinatesToChange = new List<Tuple<int, int>>(); // Fill the coordinates to change
            for (int i = 0; i < Factory.x + 1; i++)
            {
                for (int j = 0; j < Factory.y + 1; j++)
                {
                    int sum = Calculation.GetSum(Reachability[i, j], i, j);
                    if (sum == 3)
                    {
                        if (Field[i, j] == 0)
                            coordinatesToChange.Add(new Tuple<int, int>(i, j));
                    }
                    else if(sum != 4)
                    {
                        if (Field[i, j] == 1)
                            coordinatesToChange.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            foreach (var pair in coordinatesToChange)
            {
                if (Field[pair.Item1, pair.Item2] == 1)
                    Field[pair.Item1, pair.Item2] = 0;
                else
                    Field[pair.Item1, pair.Item2] = 1;
            }
            sw.Stop();
            return coordinatesToChange;
        }
    }
}
