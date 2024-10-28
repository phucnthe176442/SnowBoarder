using UnityEngine;

namespace snow_boarder
{
    public static class TransformExtensions
    {
        public static void ResetLocal(this Transform transform)
        {
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
        }
    }
}