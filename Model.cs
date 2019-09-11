using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralModelEngine
{
    public class StructuralModel
    {

        public StructuralModel()
        {
            nodes = new List<Node>();
        }

        public List<Node> nodes;

        public Node AddNode(float x, float y, float z)
        {
            var n = new Node(++nodeindexer);
            n.x = x;
            n.y = y;
            n.z = z;

            nodes.Add(n);
            return n;
        }

        public void ToView()
        {
            
        }


        private int nodeindexer = 0;
        
    }
}
