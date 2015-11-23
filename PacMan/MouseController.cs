using Rhino;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using Rhino.UI;
using System;

namespace PacMan
{
    internal class MouseController : MouseCallback
    {
        private readonly SelectableObject[] _objects;
        private SelectableObject _lastObject;
        private Line _frustrum;

        internal event EventHandler<MouseControllerEventArgs> OnChange;
        internal event EventHandler<MouseControllerEventArgs> OnMouseClick;
        internal event EventHandler<MouseControllerEventArgs> OnMouseRelease;

        internal MouseController(SelectableObject[] objects)
        {
            _objects = objects;
        }

        private SelectableObject GetObject()
        {
            foreach (var obj in _objects)
            {
                Interval interval;
                if (Intersection.LineBox(_frustrum, obj.Box, RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out interval))
                {
                    return obj;
                }
            }

            return null;
        }

        protected override void OnMouseMove(MouseCallbackEventArgs e)
        {
            e.View.ActiveViewport.GetFrustumLine(e.ViewportPoint.X, e.ViewportPoint.Y, out _frustrum);

            var currentObject = GetObject();
            if (currentObject != _lastObject)
            {
                e.Cancel = true;
                OnChange?.Invoke(this, new MouseControllerEventArgs(currentObject));
            }

            _lastObject = currentObject;

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseCallbackEventArgs e)
        {
            var currentObject = GetObject();
            if (currentObject != null)
            {
                e.Cancel = true;
                OnMouseClick?.Invoke(this, new MouseControllerEventArgs(currentObject));
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseCallbackEventArgs e)
        {
            OnMouseRelease?.Invoke(this, new MouseControllerEventArgs(GetObject()));
            base.OnMouseUp(e);
        }


    }
}
