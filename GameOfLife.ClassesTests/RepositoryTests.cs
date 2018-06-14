using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameOfLife.Classes.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        [TestMethod()]
        public void Offset_Does_Not_Exceed_Axis_Length()
        {
            // Arrange
            int x = 100;
            int y = 100;

            List<Pattern> patterns = new List<Pattern>();

            // Act
            foreach (var path in Directory.GetFiles("../../../GameOfLife.Classes/Patterns"))
            {
                var pattern = PatternFits(path, x, y);
                if (pattern.Fits)
                {
                    patterns.Add(DecodePattern(path, pattern));
                }
            }

            // Assert
            Assert.IsTrue(patterns.All(patt => !patt.Fits || patt.FieldXOffset <= x && patt.FieldYOffset <= y));
        }

        private Pattern PatternFits(string patternFilePath, int x, int y)
        {
            StreamReader reader = new StreamReader(patternFilePath);
            string infoString = reader.ReadLine(),
                name = string.Empty;
            while (infoString.StartsWith("#"))
            {
                if (infoString.StartsWith("#N"))
                {
                    name = infoString.Substring(3);
                }
                infoString = reader.ReadLine();
            }

            int patternXDimention = int.Parse(infoString.Split('=', ',')[1]),
                patternYDimention = int.Parse(infoString.Split('=', ',')[3]);
            if (((x + 1) > patternXDimention + 10 & (y + 1) > patternYDimention + 10))
                return new Pattern()
                {
                    Fits = true,
                    Name = name,
                    FieldXOffset = (x + 1) / 2 - patternXDimention / 2,
                    FieldYOffset = (y + 1) / 2 - patternYDimention / 2,
                    LivingCells = new List<Tuple<int, int>>()
                };
            else
                return new Pattern()
                {
                    Fits = false
                };
        }

        private Pattern DecodePattern(string patternFilePath, Pattern pattern)
        {
            StreamReader reader = new StreamReader(patternFilePath);
            string beginString = reader.ReadLine();
            while (beginString.StartsWith("#"))
            {
                beginString = reader.ReadLine();
            }
            string patternRLE = reader.ReadToEnd(),
                occurrencesCounter = "";
            int line = 0,
            linePosition = 0;

            foreach (char c in patternRLE)
            {
                if ("1234567890".Contains(c))
                {
                    occurrencesCounter += c;
                }
                else
                {
                    if (c == '$')
                    {
                        if (String.IsNullOrEmpty(occurrencesCounter))
                        {
                            line++;
                            linePosition = 0;
                        }
                        else
                        {
                            for (int stringShift = 0; stringShift < int.Parse(occurrencesCounter); stringShift++)
                            {
                                line++;
                                linePosition = 0;
                            }
                        }
                        occurrencesCounter = string.Empty;
                    }
                    else if (c == 'b')
                    {
                        for (int deadCells = 0; deadCells < (String.IsNullOrEmpty(occurrencesCounter) ? 1 : int.Parse(occurrencesCounter)); deadCells++)
                        {
                            linePosition++;
                        }
                        occurrencesCounter = string.Empty;
                    }
                    else if (c == 'o')
                    {
                        for (int aliveCells = 0; aliveCells < (String.IsNullOrEmpty(occurrencesCounter) ? 1 : int.Parse(occurrencesCounter)); aliveCells++)
                        {
                            pattern.LivingCells.Add(new Tuple<int, int>(linePosition + pattern.FieldXOffset, line + pattern.FieldYOffset));
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