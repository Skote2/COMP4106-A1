using System;

namespace A1 {
    abstract class Creature : Playable {
        public int x { get; set; }
        public int y { get; set; }
        protected Board.direction face;
        public Board.direction Face { get { return face; } }

        public Creature () : this(Board.direction.up) {}
        public Creature (Board.direction dirFace) { face = dirFace; }
        public Creature (int setX, int setY) : this(Board.direction.up) {
            x = setX;
            y = setY;
        }
        public Creature (Creature c) : this(c.x, c.y) {
            face = c.face;
        }

        /////////////
        // Methods //
        /////////////
        abstract public bool move (Board.direction dir);
        public void setCords (int setX, int setY) {
            x = setX;
            y = setY;
        }

        //////////////////////////
        // Operator Overloading //
        //////////////////////////
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
        override public bool Equals (object o) {
            if (o == null)
                return false;
            return o.GetType() == typeof(Board) ? this == (Creature)o : false;
        }
        private int hashing(int hash, int val) { return (hash*5 + val)^val; }
        override public int GetHashCode () {
            int hash = 7;
            hash += hashing(hash, x);
            hash += hashing(hash, y);
            return hash;
        }
    }
}