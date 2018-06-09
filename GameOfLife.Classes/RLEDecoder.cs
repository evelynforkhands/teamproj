using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    public class RLEDecoder
    {
        public static bool PatternFits(string patternFilePath, int x, int y)
        {
            StreamReader reader = new StreamReader(patternFilePath);
            string infoString = reader.ReadLine();
            int patternXDimention = int.Parse(infoString.Split('=', ',')[1]),// need to think of a way to avoid doing this again in DecodePattern if a particular pattern fits
                patternYDimention = int.Parse(infoString.Split('=', ',')[3]);
            return (x > patternXDimention + 10 & y > patternYDimention + 10);
        }

        public static int[,] DecodePattern(string patternFilePath)
        {
            StreamReader reader = new StreamReader(patternFilePath);
            string infoString = reader.ReadLine();
            while(infoString.StartsWith("#"))
            {
                infoString = reader.ReadLine();
            }

            string patternRLE = reader.ReadToEnd(),
                occurrencesCounter = "";
            int patternXDimension = int.Parse(infoString.Split('=', ',')[1]),
                patternYDimension = int.Parse(infoString.Split('=', ',')[3]),
                line = 0,
                linePosition = 0;

            int[,] pattern = new int[patternYDimension, patternXDimension];

            foreach (char c in patternRLE)
            {
                if("1234567890".Contains(c))
                {
                    occurrencesCounter += c;
                }
                else
                {
                    if(c == '$')
                    {
                        if (String.IsNullOrEmpty(occurrencesCounter))
                        {
                            line++;
                            linePosition = 0;
                        }
                        else
                        {
                            while (line < int.Parse(occurrencesCounter))
                            {
                                line++;
                                linePosition = 0;
                            }
                        }
                        occurrencesCounter = string.Empty;
                    }
                    else if(c == 'b')
                    {
                        for (int deadCells = 0; deadCells < (String.IsNullOrEmpty(occurrencesCounter) ? 1 : int.Parse(occurrencesCounter)); deadCells++)
                        {
                            linePosition++;
                        }
                        occurrencesCounter = string.Empty;
                    }
                    else if(c =='o')
                    {
                        for (int aliveCells = 0; aliveCells < (String.IsNullOrEmpty(occurrencesCounter) ? 1 : int.Parse(occurrencesCounter)); aliveCells++)
                        {
                            pattern[line, linePosition] = 1;
                            linePosition++;
                        }
                        occurrencesCounter = string.Empty;
                    }
                }
            }
            return pattern;
        }
    }
}
