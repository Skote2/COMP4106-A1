using System;
using System.Collections.Generic;


namespace A1 {
    class Board {
        public enum direction {up=0, down=1, left=2, right=3, up2right=4, up2left=5, upRight2=6, upLeft2=7, downRight=8, downLeft=9}
        private bool original;
        private int xDim;
        private int yDim;
        
        private Creature[,] grid;
        private List<Ant> ants = new List<Ant>();
        private List<Spider> spiders = new List<Spider>();

        public Board () : this(15, 15) {}
        public Board (int width, int height) : this(width, height, 1, 1) {}
        public Board (int width, int height, int numSpiders, int numAnts) {
            original = true;
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
            original = false;
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
         * returns true in any of the 3:
         *   - spider eats ant (same cords)
         *   - spider goes off of screen (game over)
         *   - Ant goes off of screen (spawn another)*/
        private bool checkEvent () {
            bool flag = false;
            if (checkWin()){//spider eats ant - same cords
                if (original){
                    replaceAnt();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Spider got the Ant");
                    Console.ForegroundColor = ConsoleColor.White;
                    flag = true;
                }
            }
            if (checkLoss()){ //spider goes off of grid
                flag = true;
                if (original)
                    Console.WriteLine("Spider went off the grid, you lose.");
            }
            for (int i = 0; i < ants.Count; ++i)
                if (ants[i].x >= xDim || ants[i].x < 0 || ants[i].y >= yDim || ants[i].y < 0){//ant goes off of grid
                    if (original)
                        replaceAnt();
                    else{//keep it in the grid so the tracking will complete, handle it going off for the AI with checks between each action on the stack of moves.
                        if (ants[i].x >= xDim)
                            ants[i].setCords(xDim-1, ants[i].y);
                        if (ants[i].x < 0)
                            ants[i].setCords(0, ants[i].y);
                        if (ants[i].y >= yDim)
                            ants[i].setCords(ants[i].x, yDim-1);
                        if (ants[i].y < 0)
                            ants[i].setCords(ants[i].x, 0);
                    }
                    flag = true;
                }
            return flag;
        }
        public bool checkLoss() {//Returns true if the spider has left the board and therefore lost
            foreach (Spider s in spiders)
                if (s.x >= xDim || s.x < 0 || s.y >= yDim || s.y < 0)
                    return true;
            return false;
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
            bool flag = !checkEvent();
            if (!original && !flag)
                return flag;
            updateGrid();
            return flag;
        }
        public void replaceAnt () {
            foreach (Spider s in spiders)
                for (int i = 0; i < ants.Count; ++i)
                    if ((s.x == ants[i].x && s.y == ants[i].y) || //check for collision
                    (ants[i].x >= xDim || ants[i].x < 0 || ants[i].y >= yDim || ants[i].y < 0))//check out of bounds
                        ants.Remove(ants[i]);
            Random r = new Random();
            Ant a = new Ant(r.Next(xDim), r.Next(yDim), (direction)r.Next(4));
            while (grid[a.x, a.y] != null)//replace until there's nothing there
                a.setCords(r.Next(xDim), r.Next(yDim));
            ants.Add(a);
            grid[a.x, a.y] = a;
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