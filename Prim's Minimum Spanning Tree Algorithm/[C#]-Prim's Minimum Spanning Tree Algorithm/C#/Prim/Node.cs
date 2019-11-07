using System;
using System.Collections.Generic;

namespace Prim
{
    class Node
    {
        public const int INFINITY = int.MaxValue;
        int id, key;
        List<Node> adjacency;
        List<Node> pi;
        List<int> weight;

        public Node(int id, int key)
        {
            this.id = id;
            this.key = key;
            adjacency = new List<Node>();
            pi = new List<Node>();
            weight = new List<int>();
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public List<Node> Adjacency
        {
            get{
                return adjacency;
            }
            set{
                adjacency = value;
            }
        }

        public List<Node> Pi
        {
            get{
                return pi;
            }
            set{
                pi = value;
            }
        }

        public List<int> Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        public void addNodeToPi(Node node)
        {
            pi.Add(node);
        }

        public void clearPi()
        {
            pi = new List<Node>();
        }
    }
}