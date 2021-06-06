using System;
namespace DCEL.Models
{
    public class Vertex
    {
        public int Tag;
        public HalfEdge IncidentEdge;
        public int X;
        public int Y;
        public double Angle;

        //----------------------------------------------------------------------
        // Name: Vertex
        // Desc: Constructor
        //----------------------------------------------------------------------
        public Vertex(int x, int y)
        {
            (X, Y, Angle) = (x, y, 0);
        }

        //----------------------------------------------------------------------
        // Name: Substract
        // Desc: Substract the components of one vertex with another vertex
        //----------------------------------------------------------------------
        public Vertex Substract(Vertex p)
        {
            return new Vertex(this.X - p.X, this.Y - p.Y);
        }

        //----------------------------------------------------------------------
        // Name: GetDistanceFromLine
        // Desc: Measures the distance between a point and a "infinite" line
        //       conformed by two points
        //----------------------------------------------------------------------
        public static double GetDistanceFromLine(Vertex linePoint1, Vertex linePoint2, Vertex point)
        {
            double numerator = ((linePoint2.X - linePoint1.X) * (linePoint1.Y - point.Y)) - ((linePoint1.X - point.X) * (linePoint2.Y - linePoint1.Y));
            if (numerator < 0)
                numerator *= -1;

            double denominator = GetDistance(linePoint1, linePoint2);

            if (denominator > 0)
                return numerator / denominator;
            return 0;
        }


        //----------------------------------------------------------------------
        // Name: GetDistanceFromLineSegment
        // Desc: Measures the distance between a point and a line segment
        // Code taken from: https://www.geeksforgeeks.org/minimum-distance-from-a-point-to-the-line-segment-using-vectors/
        //----------------------------------------------------------------------
        public static double GetDistanceFromLineSegment(Vertex A, Vertex  B, Vertex E)
        {
            // vector AB
            Vertex AB = B.Substract(A);

            // vector BP
            Vertex BE = E.Substract(B);

            // vector AP
            Vertex AE = E.Substract(A);

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

        //----------------------------------------------------------------------
        // Name: GetDistance
        // Desc: Dot product between two points (vertices)
        //----------------------------------------------------------------------
        public static double GetDistance(Vertex p1, Vertex p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        //----------------------------------------------------------------------
        // Name: CrossProduct
        // Desc: Cross product between two points (vertices)
        //----------------------------------------------------------------------
        public static int CrossProduct(Vertex p1, Vertex p2)
        {
            return p1.X * p2.Y - p2.X * p1.Y;
        }

        //----------------------------------------------------------------------
        // Name: GetDirection
        // Desc: Gets the direction of turning between three vertices, if it
        //       returns = 0, the direction is colinear, if it is < 0 it is a
        //       left turn, and if it is > 0 it is a right turn
        //----------------------------------------------------------------------
        public static int GetDirection(Vertex p1, Vertex p2, Vertex p3)
        {
            return CrossProduct(p3.Substract(p1), p2.Substract(p1));
        }

        //----------------------------------------------------------------------
        // Name: IsTurningLeft
        // Desc: Returns true the direction of turning between three vertices,
        //       is left
        //----------------------------------------------------------------------
        public static bool IsTurningLeft(Vertex p1, Vertex p2, Vertex p3)
        {
            return GetDirection(p1, p2, p3) < 0;
        }

        //----------------------------------------------------------------------
        // Name: IsTurningRight
        // Desc: Returns true the direction of turning between three vertices,
        //       is right
        //----------------------------------------------------------------------
        public static bool IsTurningRight(Vertex p1, Vertex p2, Vertex p3)
        {
            return GetDirection(p1, p2, p3) > 0;
        }

        //----------------------------------------------------------------------
        // Name: IsCollinear
        // Desc: Returns true the direction of turning between three vertices,
        //       is collinear
        //----------------------------------------------------------------------
        public static bool IsCollinear(Vertex p1, Vertex p2, Vertex p3)
        {
            return GetDirection(p1, p2, p3) == 0;
        }
    }
}
