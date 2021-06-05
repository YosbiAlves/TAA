using System;
using System.Collections.Generic;

namespace DCEL.Models
{
    public class Field
    {
        public readonly DCEL DCEL = new DCEL();

        public double Width { get; private set; }
        public double Height { get; private set; }

        public void Resize(double width, double height) =>
            (Width, Height) = (width, height);
     
        public void AddVertex(int x, int y)
        {
            // Preventing repeated points
            foreach( var v in DCEL.Vertices)
            {
                if (v.X == x && v.Y == y)
                    return; // vertex repeated
            }

            Vertex vertex = new Vertex(x, y);

            DCEL.Add(vertex);
        }
    }
}
