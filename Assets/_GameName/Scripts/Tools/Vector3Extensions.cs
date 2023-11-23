using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 WithoutZ(this Vector3 vector)
    {
        vector.z = 0;
        return vector;
    }

    public static Vector3 WithZ(this Vector3 vector, float newZ)
    {
        vector.z = newZ;
        return vector;
    }
}
