using System;
namespace DCEL.Models
{
    public class Vertex
    {
        public int Tag;
        public HalfEdge IncidentEdge;
        public int X;
        public int Y;
        public Vertex(int x, int y)
        {
            (X, Y) = (x, y);
        }

        public Vertex Substract(Vertex p)
        {
            return new Vertex(this.X - p.X, this.Y - p.Y);
        }

        /*public static int sqr(int x) { return x * x; }
        public static int dist2(Vertex v, Vertex w) { return sqr(v.X - w.X) + sqr(v.Y - w.Y); }

        public static int distToSegmentSquared(Vertex p, Vertex v, Vertex w)
        {
            var l2 = dist2(v, w);
            if (l2 == 0) return dist2(p, v);
            var t = ((p.X - v.X) * (w.X - v.X) + (p.Y - v.Y) * (w.Y - v.Y)) / l2;
            t = Math.Max(0, Math.Min(1, t));
            return dist2(p, new Vertex(v.X + t * (w.X - v.X), v.Y + t * (w.Y - v.Y)));
        }
        public static double distToSegment(Vertex p, Vertex v, Vertex w) { return Math.Sqrt(distToSegmentSquared(p, v, w)); }
        public static double GetDistanceFromLine(Vertex linePoint1, Vertex linePoint2, Vertex point)
        {
            double numerator = ((linePoint2.X - linePoint1.X) * (linePoint1.Y - point.Y)) - ((linePoint1.X - point.X) * (linePoint2.Y - linePoint1.Y));
            if (numerator < 0)
                numerator *= -1;

            double denominator = GetDistance(linePoint1, linePoint2);
            if (denominator <= 0)
                throw new ArgumentException("Denominator < 0." + denominator);//denominator *= -1;

            if (denominator > 0)
                return numerator / denominator;
            return 0;
            //return 0;
        }*/


        // code from https://www.geeksforgeeks.org/minimum-distance-from-a-point-to-the-line-segment-using-vectors/
        public static double minDistance(Vertex A, Vertex  B, Vertex E)
        {

            // vector AB
            Vertex AB = B.Substract(A);
            //AB.F = B.F - A.F;
            //AB.S = B.S - A.S;

            // vector BP
            Vertex BE = E.Substract(B);
            //BE.F = E.F - B.F;
            //BE.S = E.S - B.S;

            // vector AP
            Vertex AE = E.Substract(A);
            //AE.F = E.F - A.F;
            //AE.S = E.S - A.S;

            // Variables to store dot product
            double AB_BE, AB_AE;

            // Calculating the dot product
            AB_BE = (AB.X * BE.X + AB.Y * BE.Y);
            AB_AE = (AB.X * AE.X + AB.Y * AE.Y);

            // Minimum distance from
            // point E to the line segment
            double reqAns = 0;

            // Case 1
            if (AB_BE > 0)
            {

                // Finding the magnitude
                double y = E.Y - B.Y;
                double x = E.X - B.X;
                reqAns = Math.Sqrt(x * x + y * y);
            }

            // Case 2
            else if (AB_AE < 0)
            {
                double y = E.Y - A.Y;
                double x = E.X - A.X;
                reqAns = Math.Sqrt(x * x + y * y);
            }

            // Case 3
            else
            {

                // Finding the perpendicular distance
                double x1 = AB.X;
                double y1 = AB.Y;
                double x2 = AE.X;
                double y2 = AE.Y;
                double mod = Math.Sqrt(x1 * x1 + y1 * y1);
                reqAns = Math.Abs(x1 * y2 - y1 * x2) / mod;
            }
            return reqAns;
        }

        public static double GetDistance(Vertex p1, Vertex p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        public static int CrossProduct(Vertex p1, Vertex p2)
        {
            return p1.X * p2.Y - p2.X * p1.Y;
        }

        public static int GetDirection(Vertex p1, Vertex p2, Vertex p3)
        {
            return CrossProduct(p3.Substract(p1), p2.Substract(p1));
        }

        public static bool IsTurningLeft(Vertex p1, Vertex p2, Vertex p3)
        {
            return GetDirection(p1, p2, p3) < 0;
        }

        public static bool IsTurningRight(Vertex p1, Vertex p2, Vertex p3)
        {
            return GetDirection(p1, p2, p3) > 0;
        }

        public static bool IsCollinear(Vertex p1, Vertex p2, Vertex p3)
        {
            return GetDirection(p1, p2, p3) == 0;
        }
    }
}
