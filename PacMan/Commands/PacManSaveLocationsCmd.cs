using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input;

namespace PacMan
{
    [System.Runtime.InteropServices.Guid("c51c228e-7238-40db-a203-e7d40e9dacda")]
    public class PacManSaveLocationsCmd : Command
    {
        public PacManSaveLocationsCmd()
        {
            Instance = this;
        }

        ///<summary>The only instance of the PacManSaveLocationsCmd command.</summary>
        public static PacManSaveLocationsCmd Instance { get; private set; }

        public override string EnglishName => "PacManSaveLocations";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {

            //var pts = PointInfo.LoadPoints(Serialized.Locations.BallPoints.test);

            //RhinoDoc.ActiveDoc.Objects.AddPoint(pts[0]);


            ObjRef[] objs;
            RhinoGet.GetMultipleObjects("Select Objects", true, ObjectType.AnyObject, out objs);
            if (objs == null || !objs.Any()) return Result.Cancel;

            var dlg = new SaveFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return Result.Cancel;

            var points = objs.Select(objRef => new PointInfo(objRef.Geometry().GetBoundingBox(true).Center)).ToList();

            points.Save(dlg.FileName);
            return Result.Success;
        }
    }
}
