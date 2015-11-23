using Rhino.Geometry;
using System.Drawing;
using Rhino.Display;
using Rhino;

namespace PacMan
{

    internal static class SelectionScreen
    {
        static readonly MouseController _mouseController;

        static readonly Transform _transformMouseOver, _transformMouseDown;
        static readonly GameObject[] _menuObjects;
        static readonly BoundingBox _box;
        static bool _exit, _enabled;
        static ElementType _currentOption;
        static bool _pressed;

        static SelectionScreen()
        {
            _transformMouseDown = Transform.Translation(Vector3d.ZAxis * -1);
            _transformMouseOver = Transform.Translation(Vector3d.ZAxis * 1);

            var menuTitle = new SelectableObject(ElementType.Drawable);
            menuTitle.AddGeoemtry(Serialized.Meshes.Menu.TitleBigBackground, Settings.MaterialOrange);
            menuTitle.AddGeoemtry(Serialized.Meshes.Menu.TitleBigBorder, Settings.MaterialRed);
            menuTitle.AddGeoemtry(Serialized.Meshes.Menu.TitleBigLetters, Settings.MaterialYellow);

            var menuOptions = new SelectableObject(ElementType.MenuOptions);
            menuOptions.AddGeoemtry(Serialized.Meshes.Menu.OptionsBackground, Settings.MaterialOrange);
            menuOptions.AddGeoemtry(Serialized.Meshes.Menu.OptionsBorder, Settings.MaterialRed);
            menuOptions.AddGeoemtry(Serialized.Meshes.Menu.OptionsLetters, Settings.MaterialYellow);

            var menuStart = new SelectableObject(ElementType.MenuStart);
            menuStart.AddGeoemtry(Serialized.Meshes.Menu.StartBacbground, Settings.MaterialOrange);
            menuStart.AddGeoemtry(Serialized.Meshes.Menu.StartBorder, Settings.MaterialRed);
            menuStart.AddGeoemtry(Serialized.Meshes.Menu.StartLetters, Settings.MaterialYellow);

            var menuExit = new SelectableObject(ElementType.MenuExit);
            menuExit.AddGeoemtry(Serialized.Meshes.Menu.ExitBacground, Settings.MaterialOrange);
            menuExit.AddGeoemtry(Serialized.Meshes.Menu.ExitBorder, Settings.MaterialRed);
            menuExit.AddGeoemtry(Serialized.Meshes.Menu.ExitLetters, Settings.MaterialYellow);

            _menuObjects = new[] { menuTitle, menuOptions, menuStart, menuExit };

            _mouseController = new MouseController(new[] { menuOptions, menuStart, menuExit });
            _mouseController.OnChange += _mouseController_OnChange;
            _mouseController.OnMouseClick += _mouseController_OnMouseClick;
            _mouseController.OnMouseRelease += _mouseController_OnMouseRelease;

            _box = new BoundingBox(-400, -400, -100, 400, 400, 100);


        }

        private static void _mouseController_OnChange(object sender, MouseControllerEventArgs e)
        {
            _currentOption = e.GameObject.ObjectType;
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private static void _mouseController_OnMouseClick(object sender, MouseControllerEventArgs e)
        {
            _currentOption = e.GameObject.ObjectType;
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private static void _mouseController_OnMouseRelease(object sender, MouseControllerEventArgs e)
        {
            _currentOption = e.GameObject.ObjectType;
            if (_currentOption != ElementType.Unset)
                _exit = true;
        }



        internal static ElementType Start()
        {
            if (_enabled) return ElementType.Unset;

            _enabled = true;
            _mouseController.Enabled = true;

            DisplayPipeline.CalculateBoundingBox += DisplayPipeline_CalculateBoundingBox;
            DisplayPipeline.PostDrawObjects += DisplayPipeline_PostDrawObjects;

            RhinoDoc.ActiveDoc.Views.Redraw();
            while (!_exit)
            {
                System.Threading.Thread.Sleep(50);
            }

            Stop();
            RhinoDoc.ActiveDoc.Views.Redraw();

            return _currentOption;
        }

        public static void Stop()
        {
            DisplayPipeline.CalculateBoundingBox -= DisplayPipeline_CalculateBoundingBox;
            DisplayPipeline.PostDrawObjects -= DisplayPipeline_PostDrawObjects;
            _exit = false;
            _enabled = false;
            _mouseController.Enabled = false;
        }

        private static void DisplayPipeline_PostDrawObjects(object sender, DrawEventArgs e)
        {
            _menuObjects.Draw(e);
        }

        private static void DisplayPipeline_CalculateBoundingBox(object sender, CalculateBoundingBoxEventArgs e)
        {
            e.IncludeBoundingBox(_box);
        }
    }
}
