namespace A1 {
    class Ant : Creature {
        public Ant () : base () {}
        public Ant (Board.direction dirFace) : base(dirFace) {}
        public Ant (short setX, short setY) : base(setX, setY) {}
        public Ant (short setX, short setY, Board.direction dirFace) : base(setX, setY) {
            face = dirFace;
        }
        
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
    }
}