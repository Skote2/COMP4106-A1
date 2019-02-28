namespace A1 {
    class Ant : Creature {
        private const byte speed = 3;
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
                    y += speed;
                    break;
                case Board.direction.down:
                    y -= speed;
                    break;
                case Board.direction.left:
                    x -= speed;
                    break;
                case Board.direction.right:
                    x += speed;
                    break;
                default:
                    return false;
            }
            return true;
        }
        
        override public bool Equals (object o) {
            if (o == null) return false;
            if ((object)(o as Ant) == null)
                return false;
            return this == (Ant)o;
        }
        private int hashing(int hash, int val) { return (hash*3 + val)^val; }
        override public int GetHashCode () {
            int hash = 13;
            hash += hashing(hash, x);
            hash += hashing(hash, y);
            hash += hashing(hash, (int)face);
            hash += hashing(hash, speed);
            return hash;
        }
    }
}