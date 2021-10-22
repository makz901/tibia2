using LiteNetLib.Utils;
using UnityEngine;

namespace Team801.Tibia2.Core.Extensions
{
    public static class SerializingExtensions
    {
        public static void Put(this NetDataWriter writer, Vector2 vector)
        {
            writer.Put(vector.x);
            writer.Put(vector.y);
        }

        public static Vector2 GetVector2(this NetDataReader reader)
        {
            return new Vector2(reader.GetFloat(), reader.GetFloat());
        }
    }
}