using System;

namespace A1 {
    abstract class Creature : Playable {
        public short x { get; set; }
        public short y { get; set; }
        protected Board.direction face;
        public Board.direction Face { get { return face; } }

        public Creature () : this(Board.direction.up) {}
        public Creature (Board.direction dirFace) {
            face = dirFace;
        }
        public Creature (short setX, short setY) : this(Board.direction.up) {
            x = setX;
            y = setY;
        }
        abstract public bool move (Board.direction dir);
        public void setCords (short setX, short setY) {
            x = setX;
            y = setY;
        }

        static public bool operator ==(Creature a, Creature b) {
            if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null)){
                if (object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
                    return true;
                return false;
            }
            if (a.GetType() != b.GetType())
                return false;
            if (a.face != b.face)
                return false;
            if (a.x != b.x || a.y != b.y)
                return false;
            return true;
        }
        static public bool operator !=(Creature a, Creature b) { return !(a==b); }
    }
}