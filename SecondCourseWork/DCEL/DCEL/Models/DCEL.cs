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
            Console.Write("going to add vertex");
            vertex.Tag = vertices.Count;

            if (vertices.Count == 1)
            {
                AddSecondVertex(vertex);
            }
            else if (vertices.Count == 2)
            {
                AddThirdVertex(vertex);
            }
            else if (vertices.Count > 2)
            {
                AddAnotherVertex(vertex);
            }

            vertices.Add(vertex);
        }

        private void AddSecondVertex(Vertex vertex)
        {
            HalfEdge halfEdge1 = new HalfEdge();
            HalfEdge halfEdge2 = new HalfEdge();
            Face firstFace = new Face();
            Vertex vertex0 = vertices[0];

            vertex0.IncidentEdge = halfEdge1;
            vertex.IncidentEdge = halfEdge2;

            halfEdge1.IncidentFace = firstFace;
            halfEdge1.Next = halfEdge2;
            halfEdge1.Origin = vertex0;
            halfEdge1.Prev = halfEdge2;
            halfEdge1.Twin = halfEdge2;

            halfEdge2.IncidentFace = firstFace;
            halfEdge2.Next = halfEdge1;
            halfEdge2.Origin = vertex;
            halfEdge2.Prev = halfEdge1;
            halfEdge2.Twin = halfEdge1;

            firstFace.Edge = halfEdge1;

            Add(halfEdge1, halfEdge2);
            Add(firstFace);
        }

        private void AddAVertex(HalfEdge halfEdge0, Vertex vertex3)
        {

            // After obtaining the closest edge
            Vertex vertex1 = halfEdge0.Origin;
            Vertex vertex2 = halfEdge0.Next.Origin;

            HalfEdge halfEdge1 = new HalfEdge();
            HalfEdge halfEdge2 = new HalfEdge();
            HalfEdge halfEdge3 = new HalfEdge();
            HalfEdge halfEdge4 = new HalfEdge();

            Face face = new Face();
            face.Edge = halfEdge1;
            faces[0].Edge = halfEdge2;

            vertex3.IncidentEdge = halfEdge1;

            halfEdge1.IncidentFace = face;
            halfEdge1.Next = halfEdge0;
            halfEdge1.Prev = halfEdge3;
            halfEdge1.Twin = halfEdge2;
            halfEdge1.Origin = vertex3;

            halfEdge3.IncidentFace = face;
            halfEdge3.Next = halfEdge1;
            halfEdge3.Prev = halfEdge0;
            halfEdge3.Twin = halfEdge4;
            halfEdge3.Origin = vertex2;

            halfEdge2.IncidentFace = faces[0];
            halfEdge2.Next = halfEdge4;
            halfEdge2.Prev = halfEdge0.Prev;
            halfEdge2.Twin = halfEdge1;
            halfEdge2.Origin = vertex1;

            halfEdge4.IncidentFace = faces[0];
            halfEdge4.Next = halfEdge0.Next;
            halfEdge4.Prev = halfEdge2;
            halfEdge4.Twin = halfEdge3;
            halfEdge4.Origin = vertex3;

            halfEdge0.IncidentFace = face;
            halfEdge0.Next.Prev = halfEdge4;
            halfEdge0.Prev.Next = halfEdge2;
            halfEdge0.Next = halfEdge3;
            halfEdge0.Prev = halfEdge1;

            Add(halfEdge1, halfEdge2);
            Add(halfEdge3, halfEdge4);

            Add(face);

        }
        private void AddThirdVertex(Vertex vertex3)
        {
            Vertex vertex1 = vertices[0];
            Vertex vertex2 = vertices[1];

            HalfEdge halfEdge1 = halfEdges[0];
            HalfEdge halfEdge2 = halfEdges[1];
            HalfEdge halfEdge3 = new HalfEdge();
            HalfEdge halfEdge4 = new HalfEdge();
            HalfEdge halfEdge5 = new HalfEdge();
            HalfEdge halfEdge6 = new HalfEdge();

            Face face = new Face();

            // Testing V1-V2-V3 turn

            bool turnLeft = Vertex.IsTurningLeft(vertex1, vertex2, vertex3);

            if (turnLeft)
            {
                // this min that halfedge2 is outside so we must change halfedge1
                face.Edge = halfEdge1;
                halfEdge1.IncidentFace = face;
                halfEdge1.Next = halfEdge3;
                halfEdge1.Prev = halfEdge5;

                halfEdge3.IncidentFace = face;
                halfEdge3.Next = halfEdge5;
                halfEdge3.Prev = halfEdge1;
                halfEdge3.Twin = halfEdge4;
                halfEdge3.Origin = vertex2;

                halfEdge5.IncidentFace = face;
                halfEdge5.Next = halfEdge1;
                halfEdge5.Prev = halfEdge3;
                halfEdge5.Twin = halfEdge6;
                halfEdge5.Origin = vertex3;

                halfEdge2.Next = halfEdge6;
                halfEdge2.Prev = halfEdge4;

                halfEdge4.IncidentFace = faces[0];
                halfEdge4.Next = halfEdge2;
                halfEdge4.Prev = halfEdge6;
                halfEdge4.Twin = halfEdge3;
                halfEdge4.Origin = vertex3;

                halfEdge6.IncidentFace = faces[0];
                halfEdge6.Next = halfEdge4;
                halfEdge6.Prev = halfEdge2;
                halfEdge6.Twin = halfEdge5;
                halfEdge6.Origin = vertex1;

                faces[0].Edge = halfEdge2;


                vertex3.IncidentEdge = halfEdge5;
            }
            else
            {
                face.Edge = halfEdge2;
                halfEdge2.IncidentFace = face;
                halfEdge2.Next = halfEdge4;
                halfEdge2.Prev = halfEdge6;

                halfEdge4.IncidentFace = face;
                halfEdge4.Next = halfEdge6;
                halfEdge4.Prev = halfEdge2;
                halfEdge4.Twin = halfEdge3;
                halfEdge4.Origin = vertex1;

                halfEdge6.IncidentFace = face;
                halfEdge6.Next = halfEdge2;
                halfEdge6.Prev = halfEdge4;
                halfEdge6.Twin = halfEdge5;
                halfEdge6.Origin = vertex3;

                halfEdge1.Next = halfEdge5;
                halfEdge1.Prev = halfEdge3;

                halfEdge3.IncidentFace = faces[0];
                halfEdge3.Next = halfEdge1;
                halfEdge3.Prev = halfEdge5;
                halfEdge3.Twin = halfEdge4;
                halfEdge3.Origin = vertex3;

                halfEdge5.IncidentFace = faces[0];
                halfEdge5.Next = halfEdge3;
                halfEdge5.Prev = halfEdge1;
                halfEdge5.Twin = halfEdge6;
                halfEdge5.Origin = vertex2;

                vertex3.IncidentEdge = halfEdge6;
            }

            Add(halfEdge3, halfEdge4);
            Add(halfEdge5, halfEdge6);

            Add(face);
        }

        private void AddAnotherVertex(Vertex vertex)
        {
            List<HalfEdge> externalEdges = new List<HalfEdge>();
            GetBorderEdges(faces[0], externalEdges);

            // Getting the closest edge to this vertice
            HalfEdge closest = null;
            double shortestDistance = double.MaxValue;
            foreach (var edge in externalEdges)
            {
                if (closest == null)
                {
                    closest = edge;
                    //shortestDistance = Vertex.GetDistanceFromLine(edge.Origin, edge.Next.Origin, vertex);
                    //shortestDistance = Vertex.distToSegment(vertex, edge.Origin, edge.Next.Origin);

                    shortestDistance = Vertex.minDistance(edge.Origin, edge.Next.Origin, vertex);
                }
                else
                {
                    //double distance = Vertex.GetDistanceFromLine(edge.Origin, edge.Next.Origin, vertex);
                    //double distance = Vertex.distToSegment(vertex, edge.Origin, edge.Next.Origin);
                    double distance = Vertex.minDistance(edge.Origin, edge.Next.Origin, vertex);
                    if (distance <= shortestDistance)
                    {
                        if (distance == shortestDistance)
                        {
                            double d1 = Vertex.GetDistanceFromLine(closest.Origin, closest.Next.Origin, vertex);
                            double d2 = Vertex.GetDistanceFromLine(edge.Origin, edge.Next.Origin, vertex);

                            if (d2 > d1)
                            {
                                shortestDistance = distance;
                                closest = edge;
                            }
                        }
                        else
                        {
                            shortestDistance = distance;
                            closest = edge;
                        }
                        
                    }

                }
            }

            AddAVertex(closest, vertex);
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
            var current = face.Edge;
            do
            {
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
