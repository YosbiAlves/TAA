using System;
using System.Collections.Generic;

namespace DCEL.Models
{
    public class Field
    {
        //public readonly List<Vertex> Vertices = new List<Vertex>();
        public readonly DCEL DCEL = new DCEL();
        public double Width { get; private set; }
        public double Height { get; private set; }

        public void Resize(double width, double height) =>
            (Width, Height) = (width, height);
     
        public void AddVertex(int x, int y)
        {
            DCEL.Add(
                    new Vertex(
                        x: x,
                        y: y
                        )
                    );
        }

        /*public void AddRandomPoints(int count = 10)
        {
            Random rand = new Random();
            
            for (int i = 0; i < count; i++)
            {
                Vertices.Add (
                    new Vertex(
                        x: (int)(Width * rand.NextDouble()),
                        y: (int)(Height * rand.NextDouble())
                        )
                    );
            }
            
        }*/
    }
}
