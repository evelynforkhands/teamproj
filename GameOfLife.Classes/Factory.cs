using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    class Factory
    {
        private static Factory _instance;
        public static Factory Instance => _instance ?? (_instance = new Factory());

        private Repository _repository;

        public Repository GetRepository() => _repository ?? (_repository = new Repository());
    }
}
