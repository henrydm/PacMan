using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Rhino.Display;
using Rhino.Geometry;

namespace PacMan
{
    public class Level
    {
        readonly AnimatedObject _pacman;
        List<AutoAnimatedObject> _gohsts;

        GameObject _maze;
        List<GameObject> _pills, _powerPills;

        public Level()
        {

            _pacman = new AnimatedObject(5);
            _pacman.AddGeoemtry(Serialized.Meshes.Game.Pac_Man, Settings.MaterialYellow);
            _pacman.AddGeoemtry(Serialized.Meshes.Game.Pac_Man_Eyes, Settings.MaterialBlack);

            _gohsts  = new List<AutoAnimatedObject>();

            var blinky = new AutoAnimatedObject(1);
            blinky.AddGeoemtry(Serialized.Meshes.Game.Ghost_Body,Settings.MaterialRed);
            blinky.AddGeoemtry(Serialized.Meshes.Game.Ghost_Mouth, Settings.MaterialBlack);
            blinky.AddGeoemtry(Serialized.Meshes.Game.Clyde_Eyes, Settings.MaterialRed);
            _gohsts.Add(blinky);


            var pinky = new AutoAnimatedObject(1);
            pinky.AddGeoemtry(Serialized.Meshes.Game.Ghost_Body, Settings.MaterialRed);
            pinky.AddGeoemtry(Serialized.Meshes.Game.Ghost_Mouth, Settings.MaterialBlack);
            pinky.AddGeoemtry(Serialized.Meshes.Game.Clyde_Eyes, Settings.MaterialRed);
            _gohsts.Add(pinky);



            //_maze = new Maze(Serialized.Meshes.Game.Maze1,Set);
            //_pacman = new GameObject(Serialized.Meshes.Game.Pacman);
            //_gohsts = new List<GameObject>(4);
            // PointInfo.LoadPoints(Serialized.Locations.BallPoints.test);

        }




        public void Start()
        {
            DisplayPipeline.PostDrawObjects += DisplayPipeline_PostDrawObjects;
            DisplayPipeline.CalculateBoundingBox += DisplayPipeline_CalculateBoundingBox;
        }

        private void DisplayPipeline_CalculateBoundingBox(object sender, CalculateBoundingBoxEventArgs e)
        {

        }

        private void DisplayPipeline_PostDrawObjects(object sender, DrawEventArgs e)
        {
            _maze.Draw(e);
            _pacman.Draw(e);
            _gohsts.Draw(e);
        }


    }
}
