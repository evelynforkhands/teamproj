using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    public class Factory
    {
        public static int x;//  both x and y are indices here
        public static int y;

        private static Factory _instance;
        public static Factory Instance => _instance ?? (_instance = new Factory());

        private Repository _repository;
        public Repository GetRepository() => _repository ?? (_repository = new Repository());

        private Generation _generation;
        public Generation GetGeneration() => _generation ?? (_generation = new Generation(x + 1, y + 1));
    }
}
