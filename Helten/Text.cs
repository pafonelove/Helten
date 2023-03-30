using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSpace;
using GameScreen;

namespace Helten
{
    // class Text for display game text on screen.
    internal class Text
    {
        static Screen display = new Screen(); // Screen class object, which show text and system messages on screen
        // Draw text on screen method.
        public void DrawText(string? location)
        {
            string path;
            if (Game.languageFlag)
                path = @"txt/text_en.txt";      // path to file, from which read location name symbol by symbol
            else
                path = @"txt/text.txt";
            IEnumerable<string> strings;    // collection for choose string for display

            switch(location)
            {
                case "Cave":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(1).Take(7);    // read file (skip - from which string start, take - how many string read)
                    break;

                case "Village":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(12).Take(9);
                    break;

                case "Market":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(25).Take(7);
                    break;

                case "Crossroad":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(36).Take(7);
                    break;

                case "Blacksmith":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(47).Take(12);
                    break;

                case "Doctor":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(63).Take(13);
                    break;

                default:
                    strings = null;
                    break;
            }

            string[] lines = Transform(strings);
            int width = FindMaxLength(lines);   // find max string width for drawing border

            DrawBorder(width, lines.Length, lines);
        }

        // Draw story text on screen method.
        public void DrawLore(int pageNumber)
        {
            string path;
            if (Game.languageFlag)
                path = @"txt/lore_en.txt";      // path to file, from which read location name symbol by symbol
            else
                path = @"txt/lore.txt";
            IEnumerable<string> strings;    // collection for choose string for display

            switch (pageNumber)
            {
                case 1:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(2).Take(9);    // read file (skip - from which string start, take - how many string read)
                    break;

                case 2:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(13).Take(10);
                    break;

                case 3:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(25).Take(8);
                    break;

                case 4:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(36).Take(14);
                    break;

                case 5:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(52).Take(11);
                    break;

                case 6:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(65).Take(10);
                    break;

                case 7:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(77).Take(6);
                    break;

                case 8:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(86).Take(14);
                    break;

                case 9:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(103).Take(14);
                    break;

                case 10:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(120).Take(8);
                    break;

                case 11:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(131).Take(12);
                    break;

                case 12:
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(146).Take(7);
                    break;

                default:
                    strings = null;
                    break;
            }

            string[] lines = Transform(strings);
            int width = FindMaxLength(lines);   // find max string width for drawing border

            DrawBorder(width, lines.Length, lines);

            ConsoleKeyInfo keypress;
            do
            {
                keypress = Console.ReadKey();
                if (keypress.KeyChar != 13)
                    display.MsgContinue();
            } while (keypress.KeyChar != 13);
        }

        //public void DrawLore_En(int pageNumber)
        //{
        //    string path = @"txt/lore_en.txt";      // path to file, from which read location name symbol by symbol
        //    IEnumerable<string> strings;    // collection for choose string for display

        //    switch (pageNumber)
        //    {
        //        case 1:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(2).Take(9);    // read file (skip - from which string start, take - how many string read)
        //            break;

        //        case 2:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(13).Take(10);
        //            break;

        //        case 3:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(25).Take(8);
        //            break;

        //        case 4:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(36).Take(14);
        //            break;

        //        case 5:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(52).Take(11);
        //            break;

        //        case 6:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(65).Take(10);
        //            break;

        //        case 7:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(77).Take(6);
        //            break;

        //        case 8:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(86).Take(14);
        //            break;

        //        case 9:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(103).Take(14);
        //            break;

        //        case 10:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(120).Take(8);
        //            break;

        //        case 11:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(131).Take(12);
        //            break;

        //        case 12:
        //            Console.WriteLine();
        //            strings = File.ReadLines(path).Skip(146).Take(7);
        //            break;

        //        default:
        //            strings = null;
        //            break;
        //    }

        //    string[] lines = Transform(strings);
        //    int width = FindMaxLength(lines);   // find max string width for drawing border

        //    DrawBorder(width, lines.Length, lines);

        //    ConsoleKeyInfo keypress;
        //    do
        //    {
        //        keypress = Console.ReadKey();
        //        if (keypress.KeyChar != 13)
        //            display.MsgContinue();
        //    } while (keypress.KeyChar != 13);
        //}

        // Transform string collection to array method.
        string[] Transform(IEnumerable<string> strings)
        {
            string[] stringLines = new string[strings.Count()];
            int i = 0;
            foreach (string s in strings)
            {
                stringLines[i++] = s;
            }

            return stringLines;
        }

        // Find max string length method.
        int FindMaxLength(string[] lines)
        {
            int max = lines[0].Length;
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Length > max)
                    max = lines[i].Length;
            }

            return max;
        }

        // Draw text with border on screen method.
        void DrawBorder(int width, int height, string[] lines)
        {
            int rows = width + 4;  // + 4 for addition capacity
            int cols = height + 2;  // + 2 for correct output

            int c = 0;  // lines counter
            for (int i = 0; i < cols; i++)
            {
                Console.Write("    ");   // left space indent
                for (int j = 0; j < rows; j++)
                {
                    if (((i == 0) & (j == 0)) || ((i == 0) & (j == rows - 1)) || ((i == cols - 1) & (j == 0)) || ((i == cols - 1) & (j == rows - 1)))
                        Console.Write("+");
                    else if (((i == 0) || (i == cols - 1)) & (j > 0) & (j < rows - 1))
                        Console.Write("-");
                    else if (((j == 0) || (j == rows - 1)) & (i > 0) & (i < cols - 1))
                        Console.Write("|");
                    else
                    {

                        lines[c] = lines[c].PadRight(rows - 3); // - 3 for correct universal output. PadRight - for add spaces in string
                        Console.Write($" {lines[c]}");          // print C-string
                        c++;                                    // c++, then break
                        Console.Write("|");
                        break;
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
