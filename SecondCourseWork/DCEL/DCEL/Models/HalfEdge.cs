using System;
namespace DCEL.Models
{
    public class HalfEdge
    {
        public Vertex Origin;
        public HalfEdge Twin;
        public HalfEdge Next;
        public HalfEdge Prev;
        public Face Face;

        public HalfEdge()
        {
            Origin = null;
            Twin = null;
            Next = null;
            Prev = null;
            Face = null;
        }
    }
}
