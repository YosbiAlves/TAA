using System;
using System.Collections.Generic;

//----------------------------------------------------------------------
// Name: Field
// Desc: This class serves as the glue to the UI and the DCEL data
//       structure.
//----------------------------------------------------------------------
namespace DCEL.Models
{
    public class Field
    {
        public readonly DCEL DCEL = new DCEL();

        public double Width { get; private set; }
        public double Height { get; private set; }

        //----------------------------------------------------------------------
        // Name: Resize
        // Desc: Called when the canvas has been resized
        //----------------------------------------------------------------------
        public void Resize(double width, double height) =>
            (Width, Height) = (width, height);


        //----------------------------------------------------------------------
        // Name: AddVertex
        // Desc: Called when we add a vertex to the canvas to insert into the
        //       DCEL
        //----------------------------------------------------------------------
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
