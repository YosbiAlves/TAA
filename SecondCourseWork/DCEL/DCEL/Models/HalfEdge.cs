using System;
namespace DCEL.Models
{
    public class HalfEdge
    {
        public int Tag;
        public Vertex Origin;
        public HalfEdge Twin;
        public HalfEdge Next;
        public HalfEdge Prev;
        public Face IncidentFace;

        public HalfEdge()
        {
            Origin = null;
            Twin = null;
            Next = null;
            Prev = null;
            IncidentFace = null;
        }
    }
}
