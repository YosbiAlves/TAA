using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DCEL.Models
{
    public class Face
    {
        public HalfEdge Edge;
        //public int PositionX;
        //public int PositionY;
        public String Tag;

        public Face()
        {
            Edge = null;
            //PositionX = 0;
            //PositionY = 0;
            Tag = null;
        }
    }
}
