using System;
using System.Collections.Generic;


namespace A1 {
    class Board {
        public enum direction {up, down, left, right, up2right, up2left, upRight2, upLeft2}
        short xDim;
        short yDim;
        private Creature[,] grid;
        List<Ant> ants = new List<Ant>();
        List<Spider> spiders = new List<Spider>();

        public Board () : this(15, 15) {}
        public Board (short width, short height) : this(width, height, 1, 1) {}
        public Board (short width, short height, short numSpiders, short numAnts) {
            xDim = width;
            yDim = height;
            grid = new Creature[xDim, yDim];
            Random r = new Random();
            Spider s;
            for (int i = 0; i < numSpiders; i++){
                s = new Spider(Convert.ToInt16(r.Next(xDim)), Convert.ToInt16(r.Next(yDim)));
                while (grid[s.x, s.y] != null)//replace on grid until there's nothing in the space
                    s.setCords(Convert.ToInt16(r.Next(xDim)), Convert.ToInt16(r.Next(yDim)));
                spiders.Add(s);
                grid[s.x, s.y] = s;
            }
            Ant a;
            for (int i = 0; i < numAnts; i++){
                a = new Ant(Convert.ToInt16(r.Next(xDim)), Convert.ToInt16(r.Next(yDim)), (direction)r.Next(4));
                while (grid[a.x, a.y] != null)//replace until there's nothing there
                    a.setCords(Convert.ToInt16(r.Next(xDim)), Convert.ToInt16(r.Next(yDim)));
                ants.Add(a);
                grid[a.x, a.y] = a;
            }
        }

        /////////////
        // Methods //
        /////////////
        /* checks and handles spider on ant, and off grid events
         * only returns false if a game ending event happens (spider walks off the grid)*/
        private bool checkEvent () {
            foreach (Spider s in spiders) {
                // TODO check collisions with ants
                // TODO check for off grid
            }
            return false;
            foreach (Ant a in ants) {
                // TODO check for off grid to spawn new
            }
        }
        public bool play(Board.direction dir) {
            foreach (Spider s in spiders)
                s.move(dir);
            foreach (Ant a in ants)
                a.move(a.Face);
            checkEvent();
            updateGrid();
            return true;
        }
        private void updateGrid () {
            grid = new Creature[xDim, yDim];
            foreach (Spider s in spiders)
                grid[s.x, s.y] = s;
            foreach (Ant a in ants)
                grid[a.x, a.y] = a;
        }

        /////////////////////////////////////////////
        // System methods, operators and overrides //
        /////////////////////////////////////////////
        override public string ToString() { // returns a visual representation of the grid
            string s = "   Y\n";
            for (int y = yDim-1; y >= 0; --y){
                s += String.Format("{0, 3}│", y+1);
                for (int x = 0; x < xDim; ++x){
                    if (grid[x,y] == null)
                        s += "  -";
                    else {
                        if (grid[x,y].GetType() == typeof(Spider))
                            s+="  S";
                        else
                            s+="  A";
                    }
                }
                s += '\n';
            }
            s += "   └";
            for (int i = 0 ; i < xDim; i++)
                s += "───";
            s += " X\n    ";
            for (int i = 0 ; i < xDim; i++)
                s += String.Format("{0,3}", i+1);
            return s + "\n";
        }

        public static bool operator ==(Board a, Board b) {
            if (a.xDim != b.xDim || a.yDim != b.yDim)
                return false;
            for (int x = 0; x < a.xDim; ++x)
                for (int y = 0; y < a.yDim; ++y)
                    if (a.grid[x,y] != b.grid[x,y])
                        return false;
            return true;
        }
        public static bool operator !=(Board a, Board b) { return !(a==b); }
        // override public bool Equals(object o) {
        //     return o.GetType() == typeof(Board) ? this == (Board)o : false;
        // }
    }
}