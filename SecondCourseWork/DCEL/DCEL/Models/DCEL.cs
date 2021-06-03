using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DCEL.Models
{
    public class DCEL
    {
        private readonly List<Vertex> vertices;
        private readonly List<Face> faces;
        private readonly List<HalfEdge> halfEdges;

        
        //private readonly Vertex infiniteVertex;

        internal DCEL()
        {
            //infiniteVertex = new Vertex (int.MaxValue, int.MaxValue);
            vertices = new List<Vertex>();//{infiniteVertex};
            faces = new List<Face>();
            halfEdges = new List<HalfEdge>();
        }

        internal void Add(Vertex vertex)
        {
            Debug.Assert(vertex != null);

            vertex.Tag = vertices.Count;
            vertices.Add(vertex);
        }

        internal void Add(Face face)
        {
            Debug.Assert(face != null);

            face.Tag = faces.Count;
            faces.Add(face);
        }

        internal void Add(HalfEdge edge1, HalfEdge edge2)
        {
            Debug.Assert(edge1 != null);
            Debug.Assert(edge2 != null);
            Debug.Assert(edge1.Twin == edge2);
            Debug.Assert(edge2.Twin == edge1);
            Debug.Assert(edge1 != edge2);

            edge1.Tag = halfEdges.Count;
            halfEdges.Add(edge1);

            edge2.Tag = halfEdges.Count;
            halfEdges.Add(edge2);
        }

        public void MakeFirstTriangle()
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                Vertex start;
                Vertex end;
                if (i < vertices.Count - 1)
                {
                    start = vertices[i];
                    end = vertices[i + 1];
                }
                else
                {
                    start = vertices[i];
                    end = vertices[0];
                }
                   

                HalfEdge edge = new HalfEdge();
                HalfEdge twinEdge = new HalfEdge();
            }
        }

        /// <summary>
        /// All of the vertices in this doubly connected edge list, including
        /// the infinite vertex.
        /// </summary>
        public IReadOnlyCollection<Vertex> Vertices { get { return vertices; } }

        /// <summary>
        /// All of the faces in this doubly connected edge list.
        /// </summary>
        public IReadOnlyCollection<Face> Faces { get { return faces; } }

        /// <summary>
        /// All of the half edges in this doubly connected edge list.
        /// </summary>
        public IReadOnlyCollection<HalfEdge> HalfEdges { get { return halfEdges; } }

        /// <summary>
        /// A special vertex "at infinity", used to connect together the edges
        /// of any infinite
        /// </summary>
        //public Vertex InfiniteVertex { get { return infiniteVertex; } }

        /// <summary>
        /// Gets all the half edges which border the specified face. For each
        /// bordering full edge, only the half edge which faces the specified
        /// face is returned.
        /// </summary>
        /// <param name="face">The face whose bordering edges will be found.</param>
        /// <param name="borderEdges">The collection to which the bordering
        /// half edges will be added to.</param>
        public static void GetBorderEdges(Face face, ICollection<HalfEdge> borderEdges)
        {
            if (face == null) throw new ArgumentNullException("face");
            if (borderEdges == null) throw new ArgumentNullException("borderEdges");

            var current = face.Edge;
            do
            {
                if (current == null)
                    throw new ArgumentException("Face's half edges are not a circularly linked list.");
                borderEdges.Add(current);
            } while ((current = current.Next) != face.Edge);
        }

        /// <summary>
        /// Gets all the vertices that compose the specified face.
        /// </summary>
        /// <param name="face">The face whose composing vertices will be found.</param>
        /// <param name="composingVertices">The collection to which the
        /// composing vertices will be added to.</param>
        public static void GetComposingVertices(Face face, ICollection<Vertex> composingVertices)
        {
            if (face == null) throw new ArgumentNullException("face");
            if (composingVertices == null) throw new ArgumentNullException("composingVertices");

            var current = face.Edge;
            do
            {
                if (current == null)
                    throw new ArgumentException("Face's half edges are not a circularly linked list.");
                composingVertices.Add(current.Origin);
            } while ((current = current.Next) != face.Edge);
        }

        /// <summary>
        /// Gets all the faces that share a border with the specified face.
        /// </summary>
        /// <param name="face">The face whose neighboring faces will be found.</param>
        /// <param name="adjacentFaces">The collection to which the neighboring
        /// faces will be added to.</param>
        public static void GetAdjacentFaces(Face face, ICollection<Face> adjacentFaces)
        {
            if (face == null) throw new ArgumentNullException("face");
            if (adjacentFaces == null) throw new ArgumentNullException("adjacentFaces");

            var current = face.Edge;
            do
            {
                if (current == null)
                    throw new ArgumentException("Face's half edges are not a circularly linked list.");
                adjacentFaces.Add(current.Twin.IncidentFace);
            } while ((current = current.Next) != face.Edge);
        }
    }
}
