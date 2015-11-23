using Rhino;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PacMan
{
    public enum ElementType { Unset, MenuStart, MenuOptions, MenuCredits, MenuExit,Drawable }
    internal static class Game
    {
        internal static void Start()
        {
            Color originalColor = Rhino.ApplicationSettings.AppearanceSettings.ViewportBackgroundColor;
            Rhino.ApplicationSettings.AppearanceSettings.ViewportBackgroundColor = Color.Black;

            var bw = new BackgroundWorker();
            bw.DoWork += (o, e) => e.Result = SelectionScreen.Start();
            bw.RunWorkerCompleted += (o, e) => RhinoApp.WriteLine(e.Result.ToString());
            bw.RunWorkerAsync();
           
        }
    }
}
