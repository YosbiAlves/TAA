using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DCEL.Models
{
    public class Face
    {
        public HalfEdge Edge;
        public int Tag;

        public Face()
        {
            Edge = null;
        }
    }
}
