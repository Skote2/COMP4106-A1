﻿using System;
using System.Collections.Generic;

namespace A1
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board(15, 15, 1, 1);
            outputToConsole(b.ToString());
            for(int i = 0; i < 3; ++i){
                Tree t = new Tree(b);
                Stack<Board.direction> s = t.findSolutionBFS();//doesn't always come up with a solution, (often there isn't one)
                if (s.Count == 0){
                    Console.WriteLine("No solution found");
                    break;
                }
                foreach (Board.direction d in s){
                    b.play(d);
                    outputToConsole(b.ToString());
                }
            }
            
            // playInConsole(b);
        }

        private static void playInConsole(Board b) {
            outputToConsole(b.ToString());
            while (!b.checkLoss()){
                ConsoleKeyInfo k = Console.ReadKey();
                
                switch(k.Key) {
                    case ConsoleKey.UpArrow:
                        b.play(Board.direction.up);
                        break;
                    case ConsoleKey.DownArrow:
                        b.play(Board.direction.down);
                        break;
                    case ConsoleKey.LeftArrow:
                        b.play(Board.direction.left);
                        break;
                    case ConsoleKey.RightArrow:
                        b.play(Board.direction.right);
                        break;
                }
                outputToConsole(b.ToString());
            }
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
