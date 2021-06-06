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

        //----------------------------------------------------------------------
        // Name: HalfEdge
        // Desc: Constructor
        //----------------------------------------------------------------------
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
