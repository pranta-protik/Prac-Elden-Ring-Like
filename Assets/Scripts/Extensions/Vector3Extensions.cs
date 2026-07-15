using UnityEngine;

namespace ERL
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        public static Vector3 Flat(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }
    }
}