using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rhino.Geometry;

namespace PacMan
{
    public class PointInfo
    {
        public Point3d Point;
        public PointInfo()
        {

        }
        public PointInfo(Stream stream)
        {
            var pointInfo = Extensions.Deserialize<PointInfo>(stream);
            if (pointInfo != null) Point = pointInfo.Point;
        }
        public PointInfo(Point3d point)
        {
            Point = point;
        }
        public static List<PointInfo> Load(Stream stream)
        {
            return Extensions.Deserialize<List<PointInfo>>(stream);
        }
        public static List<PointInfo> Load(byte[] bytes)
        {
            return Extensions.Deserialize<List<PointInfo>>(new MemoryStream(bytes));
        }
        public static List<Point3d> LoadPoints(byte[] bytes)
        {
            return Load(bytes).Select(pointInfo => pointInfo.Point).ToList();        
        }
    }
}
