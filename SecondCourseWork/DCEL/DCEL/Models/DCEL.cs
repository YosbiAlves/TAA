using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DCEL.Models
{
    public class DCEL
    {
        public readonly List<Vertex>    Vertices;
        public readonly List<Face>      Faces;
        public readonly List<HalfEdge>  HalfEdges;

        public DCEL()
        {
            Vertices    = new List<Vertex>();
            Faces       = new List<Face>();
            HalfEdges   = new List<HalfEdge>();
        }

        public void Add(Vertex vertex)
        {
            Console.Write("going to add vertex");
            vertex.Tag = Vertices.Count;

            if (Vertices.Count == 1)
            {
                AddSecondVertex(vertex);
            }
            else if (Vertices.Count == 2)
            {
                AddThirdVertex(vertex);
            }
            else if (Vertices.Count > 2)
            {
                AddAnotherVertex(vertex);
            }

            Vertices.Add(vertex);
        }

        private void AddSecondVertex(Vertex vertex)
        {
            HalfEdge halfEdge1 = new HalfEdge();
            HalfEdge halfEdge2 = new HalfEdge();
            Face firstFace = new Face();
            Vertex vertex0 = Vertices[0];

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
        
        private void AddThirdVertex(Vertex vertex3)
        {
            Vertex vertex1 = Vertices[0];
            Vertex vertex2 = Vertices[1];

            HalfEdge halfEdge1 = HalfEdges[0];
            HalfEdge halfEdge2 = HalfEdges[1];
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

                halfEdge4.IncidentFace = Faces[0];
                halfEdge4.Next = halfEdge2;
                halfEdge4.Prev = halfEdge6;
                halfEdge4.Twin = halfEdge3;
                halfEdge4.Origin = vertex3;

                halfEdge6.IncidentFace = Faces[0];
                halfEdge6.Next = halfEdge4;
                halfEdge6.Prev = halfEdge2;
                halfEdge6.Twin = halfEdge5;
                halfEdge6.Origin = vertex1;

                Faces[0].Edge = halfEdge2;


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

                halfEdge3.IncidentFace = Faces[0];
                halfEdge3.Next = halfEdge1;
                halfEdge3.Prev = halfEdge5;
                halfEdge3.Twin = halfEdge4;
                halfEdge3.Origin = vertex3;

                halfEdge5.IncidentFace = Faces[0];
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
            GetBorderEdges(Faces[0], externalEdges);

            // Getting the closest edge to this vertice
            HalfEdge closest = null;
            double shortestDistance = double.MaxValue;
            foreach (var edge in externalEdges)
            {
                if (closest == null)
                {
                    closest = edge;
                    shortestDistance = Vertex.GetDistanceFromLineSegment(edge.Origin, edge.Next.Origin, vertex);
                }
                else
                {
                    double distance = Vertex.GetDistanceFromLineSegment(edge.Origin, edge.Next.Origin, vertex);
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
            Faces[0].Edge = halfEdge2;

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

            halfEdge2.IncidentFace = Faces[0];
            halfEdge2.Next = halfEdge4;
            halfEdge2.Prev = halfEdge0.Prev;
            halfEdge2.Twin = halfEdge1;
            halfEdge2.Origin = vertex1;

            halfEdge4.IncidentFace = Faces[0];
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

        public void Add(Face face)
        {
            Debug.Assert(face != null);

            face.Tag = Faces.Count;
            Faces.Add(face);
        }

        public void Add(HalfEdge edge1, HalfEdge edge2)
        {
            Debug.Assert(edge1 != null);
            Debug.Assert(edge2 != null);
            Debug.Assert(edge1.Twin == edge2);
            Debug.Assert(edge2.Twin == edge1);
            Debug.Assert(edge1 != edge2);

            edge1.Tag = HalfEdges.Count;
            HalfEdges.Add(edge1);

            edge2.Tag = HalfEdges.Count;
            HalfEdges.Add(edge2);
        }

        public void GetBorderEdges(Face face, ICollection<HalfEdge> borderEdges)
        {
            HalfEdge current = face.Edge;
            do
            {
                borderEdges.Add(current);
            } while ((current = current.Next) != face.Edge);
        }

        public List<Vertex> GetCCWComposingVertices()
		{
            
            List<Vertex> vertices = new List<Vertex>();
            

            if (Vertices.Count >= 3) {
                Face face = Faces[0];
                var current = face.Edge;
			    do
			    {
				    vertices.Add(current.Origin);
			    } while ((current = current.Prev) != face.Edge);
                return vertices;
            }
            
            return vertices;
            
            
		}
    }
}
