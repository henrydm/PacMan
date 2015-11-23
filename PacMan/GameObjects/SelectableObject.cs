namespace PacMan
{
    public class SelectableObject: GameObject
    {
        public ElementType ObjectType { get; }

        public SelectableObject(ElementType objectType)
        {
            ObjectType = objectType;
        }
    }
}