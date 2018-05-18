﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    class Repository
    {
        public List<Pattern> Patterns { get; set; }
        public bool[,] CurrentGeneration { get; set; }
        public bool[,] FutureGeneration { get; set; }
    }
}