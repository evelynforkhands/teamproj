using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes.Helpers
{
    public class Calculation
    {
        private Exception exception = new Exception("👎");
        
        public int GetSum(Location reachability, int i, int j)
        {
            return GetItself(i, j) + GetRight(reachability, i, j) + GetTopRight(reachability, i, j) + GetTop(reachability, i, j) + GetTopLeft(reachability, i, j) + GetLeft(reachability, i, j) + GetBottomLeft(reachability, i, j) + GetBottom(reachability, i, j) + GetBottomRight(reachability, i, j);
        }

        private int GetItself(int i, int j)// 1
        {
            return Factory.Instance.GetGeneration().Field[i, j];
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
                    return Factory.Instance.GetGeneration().Field[i, j + 1];
                case Location.TopRight:
                case Location.Right:
                case Location.BottomRight:
                    return Factory.Instance.GetGeneration().Field[i, 0];
                default:
                    throw exception;

            }

        }

        private int GetBottomRight(Location reachability, int i, int j)// 3
        {
            switch(reachability)
            {
                case Location.Center:
                case Location.Top:
                case Location.TopLeft:
                case Location.Left:
                    return Factory.Instance.GetGeneration().Field[i + 1, j + 1];
                case Location.BottomLeft:
                case Location.Bottom:
                    return Factory.Instance.GetGeneration().Field[0, j + 1];
                case Location.TopRight:
                case Location.Right:
                    return Factory.Instance.GetGeneration().Field[i+1,0];
                case Location.BottomRight:
                    return Factory.Instance.GetGeneration().Field[0, 0];
                default:
                    throw exception;
            }
        }

        private int GetBottom(Location reachability, int i, int j)// 4
        {
            switch(reachability)
            {
                case Location.Center:
                case Location.Top:
                case Location.TopLeft:
                case Location.Left:
                case Location.Right:
                case Location.TopRight:
                    return Factory.Instance.GetGeneration().Field[i + 1, j];
                case Location.BottomLeft:
                case Location.Bottom:
                case Location.BottomRight:
                    return Factory.Instance.GetGeneration().Field[0, j];
                default:
                    throw exception;
            }
            
        }

        private int GetBottomLeft(Location reachability, int i, int j)// 5
        {
            switch(reachability)
            {
                case Location.Center:
                case Location.Top:
                case Location.TopRight:
                case Location.Right:
                    return Factory.Instance.GetGeneration().Field[i + 1, j - 1];
                case Location.BottomRight:
                case Location.Bottom:
                    return Factory.Instance.GetGeneration().Field[0, j - 1];
                case Location.BottomLeft:
                    return Factory.Instance.GetGeneration().Field[0, Factory.y];
                case Location.Left:
                case Location.TopLeft:
                    return Factory.Instance.GetGeneration().Field[i + 1, Factory.y];
                default:
                    throw exception;
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
                    return Factory.Instance.GetGeneration().Field[i, j - 1];
                case Location.BottomLeft:
                case Location.Left:
                case Location.TopLeft:
                    return Factory.Instance.GetGeneration().Field[i, Factory.y];
                default:
                    throw exception;
            }
        }

        private int GetTopLeft(Location reachability, int i, int j)// 7 
        {
            switch(reachability)
            {
                case Location.Center:
                case Location.Right:
                case Location.BottomRight:
                case Location.Bottom:
                    return Factory.Instance.GetGeneration().Field[i - 1, Factory.y];
                case Location.BottomLeft:
                case Location.Left:
                    return Factory.Instance.GetGeneration().Field[i - 1, Factory.y];
                case Location.TopLeft:
                    return Factory.Instance.GetGeneration().Field[i - 1, j - 1];
                case Location.Top:
                case Location.TopRight:
                    return Factory.Instance.GetGeneration().Field[Factory.x, j - 1];
                default:
                    throw exception;
            }
        }

        private int GetTop(Location reachability, int i, int j)// 8
        {
            switch(reachability)
            {
                case Location.Center:
                case Location.Left:
                case Location.BottomLeft:
                case Location.Bottom:
                case Location.BottomRight:
                case Location.Right:
                    return Factory.Instance.GetGeneration().Field[i - 1, j];
                case Location.Top:
                case Location.TopRight:
                case Location.TopLeft:
                    return Factory.Instance.GetGeneration().Field[Factory.x, j];
                default:
                    throw exception;

            }
        }

        private int GetTopRight(Location reachability, int i, int j)// 9
        {
            switch(reachability)
            {
                case Location.Center:
                case Location.Left:
                case Location.BottomLeft:
                case Location.Bottom:
                    return Factory.Instance.GetGeneration().Field[i - 1, j + 1];
                case Location.BottomRight:
                case Location.Right:
                    return Factory.Instance.GetGeneration().Field[i - 1, 0];
                case Location.TopLeft:
                case Location.Top:
                    return Factory.Instance.GetGeneration().Field[Factory.x, j + 1];
                case Location.TopRight:
                    return Factory.Instance.GetGeneration().Field[Factory.x, 0];
                default:
                    throw exception;
            }
        }
    }
}
