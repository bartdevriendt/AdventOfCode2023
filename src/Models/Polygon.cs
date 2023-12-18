using System.Drawing;
using System.Numerics;

namespace AOC2023.Models;


public static class Vector2Extensions
{
    public static float Magnitude(this Vector2 v)
    {
        return (float)System.Math.Sqrt(v.X * v.X + v.Y * v.Y);
    }
}

public class Polygon
{
    private List<(double X, double Y)> polygon = new();

    private const float margin = 0.0001f;
    
    public void AddPoint((double X, double Y) p)
    {
        polygon.Add(p);
    }

    public double GetArea()
    {
        var n = polygon.Count;
        var result = 0.0;
        for (var i = 0; i < n - 1; i++)
        {
            result += polygon[i].Y * polygon[i + 1].X - polygon[i + 1].Y * polygon[i].X;
        }
 
        result = Math.Abs(result + polygon[n - 1].Y * polygon[0].X - polygon[0].Y * polygon[n - 1].X) / 2.0;
        return result;
    }
    
    // private float DistancePointLine2D(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    // {
    //     return (ProjectPointLine2D(point, lineStart, lineEnd) - point).Magnitude();
    // }
    // private Vector2 ProjectPointLine2D(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    // {
    //     Vector2 rhs = point - lineStart;
    //     Vector2 vector2 = lineEnd - lineStart;
    //     float magnitude = vector2.Magnitude();
    //     Vector2 lhs = vector2;
    //     if (magnitude > 1E-06f)
    //     {
    //         lhs = (Vector2)(lhs / magnitude);
    //     }
    //     float num2 = Math.Clamp(Vector2.Dot(lhs, rhs), 0f, magnitude);
    //     return (lineStart + ((Vector2)(lhs * num2)));
    // }
    //
    // private float ClosestDistanceToPolygon(Vector2 point)
    // {
    //     int nvert = polygon.Count;
    //     int i, j = 0;
    //     float minDistance = float.MaxValue;
    //     for (i = 0, j = nvert - 1; i < nvert; j = i++)
    //     {
    //         float distance = DistancePointLine2D(point, polygon[i], polygon[j]);
    //         minDistance = Math.Min(minDistance, distance);
    //     }
    //
    //     return minDistance;
    // }
    //
    // public bool IsPointInPolygon(Vector2 p)
    // {
    //     
    //     // check if point is inside or on the boundary of the polygon
    //     
    //     // https://stackoverflow.com/questions/217578/how-can-i-determine-whether-a-2d-point-is-within-a-polygon
    //     // https://stackoverflow.com/questions/8721406/how-to-determine-if-a-point-is-inside-a-2d-convex-polygon
    //     // https://stackoverflow.com/questions/22521982/check-if-point-inside-a-polygon
    //     // https://stackoverflow.com/questions/8721406/how-to-determine-if-a-point-is-inside-a-2d-convex-polygon
    //     // https://stackoverflow.com/questions/22521982/check-if-point-inside-a-polygon
    //     // https://stackoverflow.com/questions/8721406/how-to-determine-if-a-point-is-inside-a-2d-convex-polygon
    //     
    //     if(ClosestDistanceToPolygon(p) < margin)
    //     {
    //         return true;
    //     }
    //
    //     float[] vertX = new float[polygon.Count];
    //     float[] vertY = new float[polygon.Count];
    //     for (int i = 0; i < polygon.Count; i++)
    //     {
    //         vertX[i] = polygon[i].X;
    //         vertY[i] = polygon[i].Y;
    //     }
    //
    //     return IsInsidePolygon3(polygon.Count, vertX, vertY, p.X, p.Y);
    //     
    //     
    //     // var crossing = 0;
    //     // var len = polygon.Count;
    //     //
    //     // for (var i = 0; i < len - 1; i++)
    //     // {
    //     //     var j = i + 1;
    //     //     //if (j == len) j = 0;
    //     //
    //     //     var p1 = polygon[i];
    //     //     var p2 = polygon[j];
    //     //
    //     //     var y1 = p1.Y;
    //     //     var y2 = p2.Y;
    //     //
    //     //     var x1 = p1.X;
    //     //     var x2 = p2.X;
    //     //
    //     //     if (Math.Abs(x1 - x2) < 0 && Math.Abs(y1 - y2) < 0)
    //     //         continue;
    //     //
    //     //     var minY = Math.Min(y1, y2);
    //     //     var maxY = Math.Max(y1, y2);
    //     //
    //     //     if (point.Y < minY || point.Y > maxY)
    //     //         continue;
    //     //
    //     //     if (Math.Abs(minY - maxY) < 0)
    //     //     {
    //     //         var minX = Math.Min(x1, x2);
    //     //         var maxX = Math.Max(x1, x2);
    //     //
    //     //         if (point.X >= minX && point.X <= maxX)
    //     //         {
    //     //             return true;
    //     //         }
    //     //         else
    //     //         {
    //     //             if (point.X < minX)
    //     //                 ++crossing;
    //     //         }
    //     //     }
    //     //     else
    //     //     {
    //     //         var x = (x2 - x1) * (point.Y - y1) / (y2 - y1) + x1;
    //     //         if (Math.Abs(x - point.X) <= 0)
    //     //             return true;
    //     //
    //     //         if (point.X < x)
    //     //         {
    //     //             ++crossing;
    //     //         }
    //     //     }
    //     // }
    //     //
    //     // return ((crossing & 1) == 0) ? false : true;
    // }
    //
    // // public bool IsPointOnPolygon(Vector2 point)
    // // {
    // //     
    // // }
    //
    // private bool IsInsidePolygon3(int nvert, float[] vertx, float[] verty, float testx, float testy)
    // {
    //     int i, j = 0;
    //     bool c = false;
    //     for (i = 0, j = nvert - 1; i < nvert; j = i++)
    //     {
    //         if (((verty[i] > testy) != (verty[j] > testy)) &&
    //             (testx < (vertx[j] - vertx[i]) * (testy - verty[i]) / (verty[j] - verty[i]) + vertx[i]))
    //             c = !c;
    //     }
    //     return c;
    // }
    //
    // public RectangleF GetBounds()
    // {
    //     int xMin = int.MaxValue;
    //     int xMax = int.MinValue;
    //     int yMin = int.MaxValue;
    //     int yMax = int.MinValue;
    //
    //
    //     foreach (var point in polygon)
    //     {
    //         if (point.X < xMin)
    //             xMin = (int)point.X;
    //         if (point.X > xMax)
    //             xMax = (int)point.X;
    //         if (point.Y < yMin)
    //             yMin = (int)point.Y;
    //         if (point.Y > yMax)
    //             yMax = (int)point.Y;
    //     }
    //
    //     return new RectangleF(xMin, yMin, xMax - xMin, yMax - yMin);
    // }
}