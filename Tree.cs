using System.Collections.Generic;

namespace A1{
    class Tree {
        class Node {
            private Node parent;
            public Node Parent { get { return parent; } }
            private List<Node> children;
            private Dictionary<Board.direction, int> association;
            private Board data;
            private int depth;
            private bool isLeaf;
            public Node (Node parentNode, Board nodeData, int nodeDepth) {
                data = nodeData;
                parent = parentNode;
                isLeaf = data.checkWin();// || data.checkLoss();
                depth = nodeDepth;
            }

            private Node evaluatePath(Board.direction d) {
                Node n = new Node(this, new Board(data), ++depth);
                n.data.play(d);
                if (!history.ContainsKey(n.data)){
                    history.Add(n.data, n.depth);
                    children.Add(n);
                    association.Add(d, children.IndexOf(n));
                }
                return n;
            }
            public List<Node> getChildren () { return children; }
            public Node getChildren (Board.direction dir) {
                if (isLeaf)//no children
                    return null;
                if (dir == Board.direction.up)//only one that won't have one
                    return null;
                if (!association.ContainsKey(dir))
                    return evaluatePath(dir);
                return children[association[dir]];
            }
        }

        Node head;
        static Dictionary<Board, int> history = new Dictionary<Board, int>();
    }
}