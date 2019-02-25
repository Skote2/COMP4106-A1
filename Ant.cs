namespace A1 {
    class Ant : Creature {
        override public void move (Board.direction dir) {
            switch (dir) {
                // TODO: make movements up-right, up-left;
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
            }
        }
    }
}