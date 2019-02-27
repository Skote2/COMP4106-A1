using System;
using System.Collections.Generic;

namespace A1
{
    class Program
    {
        static void Main(string[] args)
        {
            Creature c = new Spider(5, 5);
            Board b = new Board();
            
            outputToConsole(b.ToString());

            ConsoleKeyInfo k = Console.ReadKey();
            
            switch(k.Key) {
                case ConsoleKey.UpArrow:
                    b.play(Board.direction.up);
                break;
                case ConsoleKey.DownArrow:
                break;
                case ConsoleKey.LeftArrow:
                break;
                case ConsoleKey.RightArrow:
                break;
            }
            outputToConsole(b.ToString());
        }

        private static void outputToConsole(string s) {
            foreach (char c in s) {
                if (c == 'S'){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(c);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (c == 'A') {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(c);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    Console.Write(c);
            }
        }
    }
}
