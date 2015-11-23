using Rhino.Display;
using Rhino.Geometry;

namespace PacMan
{
    public class AnimatedObject : GameObject
    {
        public Vector3d Direction;
        public double Speed;
        public Transform Transform;

        public AnimatedObject(double speed)
        {
            Speed = speed;
        }

        public void SetDirection(Vector3d direction)
        {
            ApplyTransform(Transform.Rotation(Direction, direction, Box.Center));
        }
        public void Move()
        {
            ApplyTransform(Transform.Translation(Direction * Speed));        
        }

        private void ApplyTransform(Transform transform)
        {
            Transform *= transform;
            Box.Transform(transform);
        }

        public override void Draw(DrawEventArgs e)
        {
            e.Display.PushModelTransform(Transform);
            base.Draw(e);
            e.Display.PopModelTransform();
        }
    }
}
