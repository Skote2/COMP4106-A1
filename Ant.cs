namespace A1 {
    class Ant : Creature {
        public Ant () : base () {}
        public Ant (Board.direction dirFace) : base(dirFace) {}
        public Ant (int setX, int setY) : base(setX, setY) {}
        public Ant (int setX, int setY, Board.direction dirFace) : base(setX, setY) {
            face = dirFace;
        }
        public Ant (Ant a) : base(a) {}
        
        override public bool move (Board.direction dir) {
            switch (dir) {
                case Board.direction.up:
                    ++y;
                    break;
                case Board.direction.down:
                    --y;
                    break;
                case Board.direction.left:
                    --x;
                    break;
                case Board.direction.right:
                    ++x;
                    break;
                default:
                    return false;
            }
            return true;
        }
        
        private int hashing(int hash, int val) { return (hash*3 + val)^val; }
        override public int GetHashCode () {
            int hash = 13;
            hash += hashing(hash, x);
            hash += hashing(hash, y);
            hash += hashing(hash, (int)face);
            return hash;
        }
    }
}