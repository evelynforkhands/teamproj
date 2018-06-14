using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Classes.Helpers;

namespace GameOfLife.Classes.Tests
{
    [TestClass()]
    public class GenerationTests
    {
        int[,] field = new int[,]
            {
                {1,0,1,0,0,0,0,1,1,0},
                {0,1,1,0,1,0,1,0,1,0},
                {1,0,1,1,1,0,0,1,0,0},
                {0,1,0,1,0,1,0,1,0,1},
                {0,1,1,0,1,0,1,0,1,1},
                {1,0,1,0,1,0,1,0,1,0},
                {1,1,0,1,0,1,1,0,1,0},
                {1,1,0,0,0,1,1,0,1,0},
                {1,1,0,0,0,0,0,0,0,1},
                {0,0,0,0,0,0,0,0,0,1},
            };

        int x = 9, y = 9;

        [TestMethod()]
        public void Are_There_Any_Cells_With_More_Than_8_Alive_Neighbours()
        {
            // Arrange
            Location[,] reachability = new Location[,]
            {
                {Location.TopLeft,Location.Top,Location.Top,Location.Top,Location.Top,Location.Top,Location.Top,Location.Top,Location.Top,Location.TopRight },
                {Location.Left,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Right },
                {Location.Left,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Right },
                {Location.Left,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Right },
                {Location.Left,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Right },
                {Location.Left,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Right },
                {Location.Left,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Right },
                {Location.Left,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Right },
                {Location.Left,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Center,Location.Right },
                {Location.BottomLeft,Location.Bottom,Location.Bottom,Location.Bottom,Location.Bottom,Location.Bottom,Location.Bottom,Location.Bottom,Location.Bottom,Location.BottomRight},
            };
            
            List<Tuple<int,int>> coordinatesToChange = new List<Tuple<int,int>>();

            List<int> sums = new List<int>();

            // Act
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    int sum = GetSum(reachability[i, j], i, j);
                    if (sum == 3)
                    {
                        if (field[i, j] == 0)
                            coordinatesToChange.Add(new Tuple<int, int>(i, j));
                    }
                    else if (sum != 4)
                    {
                        if (field[i, j] == 1)
                            coordinatesToChange.Add(new Tuple<int, int>(i, j));
                    }
                    sums.Add(sum);
                }
            }
            // Assert
            Assert.IsFalse(sums.Any(sum => sum > 9));
        }

        private int GetSum(Location reachability, int i, int j)
        {
            return GetItself(i, j) + GetRight(reachability, i, j) + GetTopRight(reachability, i, j) + GetTop(reachability, i, j) + GetTopLeft(reachability, i, j) + GetLeft(reachability, i, j) + GetBottomLeft(reachability, i, j) + GetBottom(reachability, i, j) + GetBottomRight(reachability, i, j);
        }

        private int GetItself(int i, int j)// 1
        {
            return field[i, j];
        }

        private int GetRight(Location reachability, int i, int j)// 2
        {
            switch (reachability)
            {
                case Location.Center:
                case Location.Left:
                case Location.BottomLeft:
                case Location.TopLeft:
                case Location.Top:
                case Location.Bottom:
                    return field[i, j + 1];
                case Location.TopRight:
                case Location.Right:
                case Location.BottomRight:
                    return field[i, 0];
                default:
                    throw new Exception();

            }

        }

        private int GetBottomRight(Location reachability, int i, int j)// 3
        {
            switch (reachability)
            {
                case Location.Center:
                case Location.Top:
                case Location.TopLeft:
                case Location.Left:
                    return field[i + 1, j + 1];
                case Location.BottomLeft:
                case Location.Bottom:
                    return field[0, j + 1];
                case Location.TopRight:
                case Location.Right:
                    return field[i + 1, 0];
                case Location.BottomRight:
                    return field[0, 0];
                default:
                    throw new Exception();
            }
        }

        private int GetBottom(Location reachability, int i, int j)// 4
        {
            switch (reachability)
            {
                case Location.Center:
                case Location.Top:
                case Location.TopLeft:
                case Location.Left:
                case Location.Right:
                case Location.TopRight:
                    return field[i + 1, j];
                case Location.BottomLeft:
                case Location.Bottom:
                case Location.BottomRight:
                    return field[0, j];
                default:
                    throw new Exception();
            }

        }

        private int GetBottomLeft(Location reachability, int i, int j)// 5
        {
            switch (reachability)
            {
                case Location.Center:
                case Location.Top:
                case Location.TopRight:
                case Location.Right:
                    return field[i + 1, j - 1];
                case Location.BottomRight:
                case Location.Bottom:
                    return field[0, j - 1];
                case Location.BottomLeft:
                    return field[0, y];
                case Location.Left:
                case Location.TopLeft:
                    return field[i + 1, y];
                default:
                    throw new Exception();
            }

        }

        private int GetLeft(Location reachability, int i, int j)// 6
        {
            switch (reachability)
            {
                case Location.Center:
                case Location.Top:
                case Location.TopRight:
                case Location.Right:
                case Location.BottomRight:
                case Location.Bottom:
                    return field[i, j - 1];
                case Location.BottomLeft:
                case Location.Left:
                case Location.TopLeft:
                    return field[i, y];
                default:
                    throw new Exception();
            }
        }

        private int GetTopLeft(Location reachability, int i, int j)// 7 
        {
            switch (reachability)
            {
                case Location.Center:
                case Location.Right:
                case Location.BottomRight:
                case Location.Bottom:
                    return field[i - 1, j - 1];
                case Location.BottomLeft:
                case Location.Left:
                    return field[i - 1, y];
                case Location.TopLeft:
                    return field[x, y];
                case Location.Top:
                case Location.TopRight:
                    return field[x, j - 1];
                default:
                    throw new Exception();
            }
        }

        private int GetTop(Location reachability, int i, int j)// 8
        {
            switch (reachability)
            {
                case Location.Center:
                case Location.Left:
                case Location.BottomLeft:
                case Location.Bottom:
                case Location.BottomRight:
                case Location.Right:
                    return field[i - 1, j];
                case Location.Top:
                case Location.TopRight:
                case Location.TopLeft:
                    return field[Factory.x, j];
                default:
                    throw new Exception();

            }
        }

        private int GetTopRight(Location reachability, int i, int j)// 9
        {
            switch (reachability)
            {
                case Location.Center:
                case Location.Left:
                case Location.BottomLeft:
                case Location.Bottom:
                    return field[i - 1, j + 1];
                case Location.BottomRight:
                case Location.Right:
                    return field[i - 1, 0];
                case Location.TopLeft:
                case Location.Top:
                    return field[Factory.x, j + 1];
                case Location.TopRight:
                    return field[Factory.x, 0];
                default:
                    throw new Exception();
            }
        }
    }
}