using System;
using System.Collections.Generic;

namespace DCEL.Models
{
    public class CCWPolygon
    {
        public readonly List<Vertex> Vertices;
        private Vertex lowerVertex;

        public CCWPolygon()
        {
            Vertices = new List<Vertex>();
        }

        public void Add(Vertex vertex)
        {
            if (Vertices.Count == 0)
            {
                lowerVertex = vertex;
                lowerVertex.Angle = 0;
            }
            else if(vertex.Y < lowerVertex.Y)
            {
                lowerVertex = vertex;
                lowerVertex.Angle = 0;
            }

            Vertices.Add(vertex);

            OrderCCW();
        }

        private void OrderCCW()
        {
            ComputePolarAngles();

            Vertices.Sort((x, y) => x.Angle.CompareTo(y.Angle));
        }

        private void ComputePolarAngles()
        {
            foreach (var vertex in Vertices)
            {
                if (vertex != lowerVertex)
                {
                    int delta_x = vertex.X - lowerVertex.X;
                    int delta_y = vertex.Y - lowerVertex.Y;
                    vertex.Angle = Math.Atan2(delta_y, delta_x);
                }
            }
        }
    }
}
