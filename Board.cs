using System;
using System.Collections.Generic;


namespace A1 {
    class Board {
        public enum direction {up = 0, down = 1, left = 2, right = 3, up2right, up2left, upRight2, upLeft2, downRight, downLeft}
        int xDim;
        int yDim;
        private Creature[,] grid;
        private List<Ant> ants = new List<Ant>();
        private List<Spider> spiders = new List<Spider>();

        public Board () : this(15, 15) {}
        public Board (int width, int height) : this(width, height, 1, 1) {}
        public Board (int width, int height, int numSpiders, int numAnts) {
            xDim = width;
            yDim = height;
            grid = new Creature[xDim, yDim];
            Random r = new Random();
            Spider s;
            for (int i = 0; i < numSpiders; i++){
                s = new Spider(r.Next(xDim), r.Next(yDim));
                while (grid[s.x, s.y] != null)//replace on grid until there's nothing in the space
                    s.setCords(r.Next(xDim), r.Next(yDim));
                spiders.Add(s);
                grid[s.x, s.y] = s;
            }
            Ant a;
            for (int i = 0; i < numAnts; i++){
                a = new Ant(r.Next(xDim), r.Next(yDim), (direction)r.Next(4));
                while (grid[a.x, a.y] != null)//replace until there's nothing there
                    a.setCords(r.Next(xDim), r.Next(yDim));
                ants.Add(a);
                grid[a.x, a.y] = a;
            }
        }
        public Board(Board oldBoard) : this(oldBoard.xDim, oldBoard.yDim){
            spiders = new List<Spider>();
            foreach (Spider s in oldBoard.spiders)
                spiders.Add(new Spider(s));
            ants = new List<Ant>();
            foreach (Ant a in oldBoard.ants)
                ants.Add(new Ant(a));
        }
        /////////////
        // Methods //
        /////////////
        /* checks and handles spider on ant, and off grid events
         * only returns true if a game ending event happens (spider walks off the grid)*/
        private bool checkEvent () {
            foreach (Spider s in spiders) {
                // TODO check collisions with ants
                // TODO check for off grid
            }
            foreach (Ant a in ants) {
                // TODO check for off grid to spawn new
            }
            return true;
        }
        public bool checkWin () { //Is the spider on the ant?
            foreach (Spider s in spiders)
                foreach (Ant a in ants)
                    if (s.x == a.x && s.y == a.y) //check cords of all ants and spiders in case one was removed from the board but still exists in the list
                        return true;
            return false;
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
        override public bool Equals(object o) {
            if (o == null)
                return false;
            return o.GetType() == typeof(Board) ? this == (Board)o : false;
        }
        private int hashing(int hash, int val) { return (hash*3 + val)^val; }
        override public int GetHashCode () {
            int hash = 666;
            hash += hashing(hash, xDim);
            hash += hashing(hash, yDim);
            foreach (Spider s in spiders)
                hash += hashing(hash, s.GetHashCode());
            foreach (Ant a in ants)
                hash += hashing(hash, a.GetHashCode());
            return hash;
        }
    }
}