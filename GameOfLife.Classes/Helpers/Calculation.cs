using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes.Helpers
{
    public class Calculation
    {
        public event Action<List<Tuple<int,int>>> CellsToBeChanged;

        public void CalculateNextGeneration()
        {
            var currentField = Factory.Instance.GetRepository().CurrentGeneration;
            bool[,] futureField; // Has to be initialized 
            var cellsChanged = new List<Tuple<int, int>>();
            // Logic goes here
            // As well as setting a future field, we'll form a list of cells with <x,y> coordinates to be changed to transfer to UI
            CellsToBeChanged?.Invoke(cellsChanged);
            // Factory.Instance.GetRepository().CurrentGeneration = futureField; // Uncomment when futureField is initialized
        }
    }
}
