using System;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Input;
using Rhino.Geometry;
using System.Windows.Forms;

namespace PacMan
{
    [System.Runtime.InteropServices.Guid("b157d619-9ee3-4a51-9f94-c0097d6faac8")]
    public class SaveMeshCmd : Command
    {
        static SaveMeshCmd _instance;
        public SaveMeshCmd()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SaveMeshCmd command.</summary>
        public static SaveMeshCmd Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "PacSave"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            ObjRef[] objs;
            RhinoGet.GetMultipleObjects("Select meshes", true, ObjectType.Mesh, out objs);

            var mesh = new Mesh();
            foreach (var obj in objs)
                mesh.Append(obj.Geometry() as Mesh);

            var meshInfo = new MeshInfo(mesh);
            var dlg = new SaveFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return Result.Cancel;
            meshInfo.Save(dlg.FileName);

            return Result.Success;
        }

      
    }
}
