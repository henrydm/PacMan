using System;

namespace PacMan
{
    public class MouseControllerEventArgs : EventArgs
    {
        public SelectableObject GameObject { get; set;}
        
        public MouseControllerEventArgs(SelectableObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}
