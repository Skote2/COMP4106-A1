namespace A1 {
    abstract class Creature : Playable {
        protected short x { get; set; }
        protected short y { get; set; }
        protected Board.direction dirFace;

        abstract public void move (Board.direction dir);
    }
}