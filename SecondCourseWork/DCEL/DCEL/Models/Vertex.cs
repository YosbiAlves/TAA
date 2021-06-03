using System;
namespace DCEL.Models
{
    public class Vertex
    {
        public int Tag;
        public HalfEdge IncidentEdge;
        public int X;
        public int Y;
        public Vertex(int x, int y)
        {
            (X, Y) = (x, y);
        }
    }
}
