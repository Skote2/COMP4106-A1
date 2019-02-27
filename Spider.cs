namespace A1 {
    class Spider : Creature {
        
        public Spider () {}
        public Spider (int setX, int setY) : base(setX, setY) {}
        public Spider (Spider s): base(s) {}


        override public bool move (Board.direction dir) {
            switch (dir) {
                case Board.direction.up:// TODO: Remove this
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
                case Board.direction.up2right: // and use the following
                    y+=2;
                    ++x;
                    break;
                case Board.direction.up2left:
                    y+=2;
                    --x;
                    break;
                case Board.direction.upRight2:
                    ++y;
                    x+=2;
                    break;
                case Board.direction.upLeft2:
                    ++y;
                    x-=2;
                    break;
                case Board.direction.downLeft:
                    --y;
                    --x;
                break;
                case Board.direction.downRight:
                    --y;
                    ++x;
                break;
                default:
                    return false;
            }
            return true;
        }

        override public bool Equals (object o) {
            if (o == null)
                return false;
            return o.GetType() == typeof(Spider) ? this == (Spider)o : false;
        }
    }
}