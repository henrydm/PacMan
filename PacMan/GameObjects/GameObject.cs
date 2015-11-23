using System;
using System.Collections.Generic;
using Rhino.Display;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;

namespace PacMan
{
    public class GameObject
    {
        public List<Tuple<Mesh, DisplayMaterial>> DisplayGeometry;
        public BoundingBox Box { get; }

        public GameObject(byte[] bytes, DisplayMaterial material)
        {
            AddGeoemtry(bytes, material);
        }

        protected GameObject()
        {
            Box = BoundingBox.Empty;
            DisplayGeometry = new List<Tuple<Mesh, DisplayMaterial>>();
        }

        public virtual void Draw(DrawEventArgs e)
        {
            foreach (var tuple in DisplayGeometry)
            {
                e.Display.DrawMeshShaded(tuple.Item1, tuple.Item2);
            }
        }

        public void AddGeoemtry(byte[] resource, DisplayMaterial material)
        {
            var mesh = MeshInfo.LoadMesh(resource);
            DisplayGeometry.Add(new Tuple<Mesh, DisplayMaterial>(mesh, material));
            Box.Union(mesh.GetBoundingBox(false));
        }

        public bool CheckCollision(GameObject otherObject)
        {
            return
                //CheckBoxInOther(Box, otherObject.Box) ||
                CheckBoxInOther(otherObject.Box, Box);
        }

        private static bool CheckBoxInOther(BoundingBox box1, BoundingBox box2)
        {
            return
                CheckIntervals(box1.Min.X, box2.Min.X, box2.Max.X) ||
                CheckIntervals(box1.Max.X, box2.Min.X, box2.Max.X) ||
                CheckIntervals(box1.Min.Y, box2.Min.Y, box2.Max.Y) ||
                CheckIntervals(box1.Max.Y, box2.Min.Y, box2.Max.Y);
        }

        private static bool CheckIntervals(double target, double min, double max)
        {
            return target > min && target < max;
        }
    }
}
