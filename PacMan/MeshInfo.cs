using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PacMan
{
    public class MeshInfo
    {
        public Point3f[] Vertices { get; set; }
        public List<int[]> Faces { get; set; }

        public MeshInfo()
        {

        }
        public MeshInfo(Mesh mesh)
        {
            mesh.Faces.ConvertQuadsToTriangles();
            Vertices = mesh.Vertices.ToArray();
            Faces = new List<int[]>();

            foreach (var face in mesh.Faces)
            {
                Faces.Add(new[] { face.A, face.B, face.C });
            }
        }

        public MeshInfo(Stream stream)
        {
            
            //var f = new XmlSerializer(typeof(MeshInfo));
            //var obj = f.Deserialize(stream);
            //var meshInfo = obj as MeshInfo;

            var meshInfo = Extensions.Deserialize<MeshInfo>(stream);
            Vertices = meshInfo.Vertices;
            Faces = meshInfo.Faces;
        }

        public static MeshInfo Load(string path)
        {
            return Extensions.Deserialize<MeshInfo>(path);
        }

        public Mesh GetRhinoMesh()
        {
            var mesh = new Mesh();
            mesh.Vertices.AddVertices(Vertices);

            foreach (var face in Faces)
            {
                mesh.Faces.AddFace(new MeshFace(face[0], face[1], face[2]));
            }

            mesh.Normals.ComputeNormals();
            mesh.FaceNormals.ComputeFaceNormals();
            mesh.UnifyNormals();
            mesh.Normals.ComputeNormals();

            var meshes = mesh.SplitDisjointPieces();

            mesh = new Mesh();
            for (int i = 0; i < meshes.Count(); i++)
            {
                if (meshes[i].SolidOrientation() == -1)
                    meshes[i].Flip(true, true, true);
                mesh.Append(meshes[i]);
            }

            Rhino.RhinoApp.WriteLine(mesh.SolidOrientation().ToString());
            return mesh;
        }
        

        public static Mesh LoadMesh(byte[] bytes)
        {
            return new MeshInfo(new MemoryStream(bytes)).GetRhinoMesh();
        }


    }
}
