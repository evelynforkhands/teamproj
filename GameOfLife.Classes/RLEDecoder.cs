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
        public static Pattern PatternFits(string patternFilePath)
        {
            StreamReader reader = new StreamReader(patternFilePath);
            string infoString = reader.ReadLine(),
                name = string.Empty;
            while (infoString.StartsWith("#"))
            {
                if(infoString.StartsWith("#N"))
                {
                    name = infoString.Substring(3);
                }
                infoString = reader.ReadLine();
            }
            
            int patternXDimention = int.Parse(infoString.Split('=', ',')[1]),
                patternYDimention = int.Parse(infoString.Split('=', ',')[3]);
            if (((Factory.x + 1) > patternXDimention + 10 & (Factory.y + 1) > patternYDimention + 10))
                return new Pattern()
                {
                    Fits = true,
                    Name = name,
                    FielfXOffset = (Factory.x + 1) / 2 - patternXDimention / 2,
                    FieldYOffset = (Factory.y + 1) / 2 - patternYDimention / 2,
                    LivingCells = new List<Tuple<int, int>>()
                };
            else
                return new Pattern()
                {
                    Fits = false
                };
        }

        public static Pattern DecodePattern(string patternFilePath, Pattern pattern)
        {
            StreamReader reader = new StreamReader(patternFilePath);
            string beginString = reader.ReadLine();
            while(beginString.StartsWith("#"))
            {
                beginString = reader.ReadLine();
            }
            string patternRLE = String.Concat(beginString,reader.ReadToEnd()),
                occurrencesCounter = "";
                int line = 0,
                linePosition = 0;
            //int[,] pattern = new int[patternYDimension, patternXDimension];

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
                            pattern.LivingCells.Add(new Tuple<int, int>(line + pattern.FielfXOffset, linePosition + pattern.FieldYOffset));
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
