﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralModelEngine
{
    class Model
    {
        
        public Model()
        {
            nodes = new List<Node>();
        }
        public List<Node> nodes;// = new List<Node>();

        public Node AddNode(float x, float y, float z)
        {
            var n = new Node(++nodeindexer);
            n.x = x;
            n.y = y;
            n.z = z;

            nodes.Add(n);
            return  n;
        }

        private int nodeindexer = 0;
        
    }
}