using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Rhino.Display;

namespace PacMan
{
    public static class Extensions
    {
        public static void Draw(this IEnumerable<GameObject> characters, DrawEventArgs e)
        {
            foreach (var character in characters)
            {
                character.Draw(e);
            }
        }

        public static T Deserialize<T>(Stream stream)
        {
            var xs = new XmlSerializer(typeof(T));
            return (T) xs.Deserialize(stream); 
        }

        public static void Dummy()
        {
            
        }

        public static T Deserialize<T>(string path)
        {
            return Deserialize<T>(File.OpenRead(path));
        }

        public static void Serialize(this object obj, string path)
        {
            var xs = new XmlSerializer(obj.GetType());
            var stream = File.CreateText(path);
            xs.Serialize(stream, obj);
            stream.Close();
            stream.Dispose();
        }
        //public static void Serialize(this object obj, Type type, string path)
        //{
        //    var f = new XmlSerializer(type);
        //    using (var stream = File.Create(path))
        //    {
        //        f.Serialize(stream, obj);
        //    }
        //}

        //public static object Deserialize(Type type, string path)
        //{
        //    using (var stream = File.OpenRead(path))
        //    {
        //        return Deserialize(type, stream);
        //    }
        //}
        //public static object Deserialize(Type type, Stream stream)
        //{
        //    return new MeshInfo(stream);
        //}

        public static void Save(this MeshInfo obj, string path)
        {
            Serialize(obj, path);
        }

        public static void Save(this IEnumerable<PointInfo> obj, string path)
        {
            Serialize(obj,path);
        }

    }
}
