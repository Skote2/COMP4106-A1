using System.Collections.Generic;
using System;

namespace A1{
    class Tree {
        public class Node {
            private Node parent;
            public Node Parent { get { return parent; } }
            private List<Node> children;
            public Dictionary<Board.direction, int> association;
            private Board data;
            private int depth;
            private bool isLeaf;
            public bool IsLeaf { get{ return isLeaf; } }
            public Node (Node parentNode, Board nodeData, int nodeDepth) {
                data = nodeData;
                parent = parentNode;
                isLeaf = data.checkWin() || data.checkLoss();
                depth = nodeDepth;
                association = new Dictionary<Board.direction, int>();
                children = new List<Node>();
            }

            private Node evaluatePath(Board.direction d) {
                Board hold = new Board(data);
                if (hold.play(d) == false)
                    return null;
                Node n = new Node(this, hold, depth+1);
                if (!history.ContainsKey(n.data)){
                    history.Add(n.data, n.depth);
                    children.Add(n);
                    association.Add(d, children.IndexOf(n));
                    
                }
                else{
                    if (history[n.data] > n.depth){
                        history.Remove(n.data);
                        history.Add(n.data, n.depth);
                    }
                    else
                        // return history[n.data];
                        return null;
                }
                return n;
            }
            public bool isWinState() { return data.checkWin(); }
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
        //////////
        // Tree //
        //////////
        private Node head;
        public Node Head { get { return head; } }
        static Dictionary<Board, int> history = new Dictionary<Board, int>();

        public Tree(Board b) {
            head = new Node(null, b, 0);
            history = new Dictionary<Board, int>();
        }

        public Stack<Board.direction> findSolutionBFS() {
            Array directions = Enum.GetValues(typeof(Board.direction));
            Node n= head;
            bool solved = false;
            
            Queue<Node> current = new Queue<Node>();
            current.Enqueue(n);
            Queue<Node> next = new Queue<Node>();
            while (!solved && current.Count != 0){//loop down until a solution is found
                foreach(Node nCur in current){
                    foreach (Board.direction d in directions){
                        Node nChild = nCur.getChildren(d);
                        if (nChild != null){
                            if (nChild.IsLeaf)
                                if (nChild.isWinState()){
                                    n = nChild;
                                    solved = true;
                                    break;
                                }
                            next.Enqueue(nChild);
                        }
                    }
                    if (solved)
                        break;
                }
                current = next;
                next = new Queue<Node>();
            }
            Stack<Board.direction> s = new Stack<Board.direction>();
            if (solved) {
                Node p;
                int i;
                //loop up from solution node and collect the decisions
                while (n.Parent != null){
                    p = n.Parent;
                    for (i = 0; i < p.getChildren().Count; ++i)
                        if (p.getChildren()[i] == n)
                            break;

                    Board.direction d = Board.direction.up;
                    foreach (KeyValuePair<Board.direction, int> dir in p.association)
                        if (dir.Value == i){
                            d = dir.Key;
                            break;
                        }
                    s.Push(d);
                    n = n.Parent;
                }
            }

            return s;
        }
        private Node DFS (Node n) {//this is private because it's the recursive loop. call the public one
            Array directions = Enum.GetValues(typeof(Board.direction));

            foreach (Board.direction d in directions){
                Node nChild = n.getChildren(d);
                if (nChild != null){
                    if (nChild.IsLeaf)
                        if (nChild.isWinState()){
                            return nChild;
                        }
                    DFS(nChild);
                }
            }
            return null;
        }
        public Stack<Board.direction> findSolutionDFS() {//Call this, it probably won't work but it has before. I think the history dictionary is broken and it can only go out one way as a result.
            Node n= head;
            
            n = DFS(n);
            
            Stack<Board.direction> s = new Stack<Board.direction>();
            if (n != null) {
                Node p;
                int i;
                //loop up from solution node and collect the decisions
                while (n.Parent != null){
                    p = n.Parent;
                    for (i = 0; i < p.getChildren().Count; ++i)
                        if (p.getChildren()[i] == n)
                            break;

                    Board.direction d = Board.direction.up;
                    foreach (KeyValuePair<Board.direction, int> dir in p.association)
                        if (dir.Value == i){
                            d = dir.Key;
                            break;
                        }
                    s.Push(d);
                    n = n.Parent;
                }
            }

            return s;
        }
    }
}