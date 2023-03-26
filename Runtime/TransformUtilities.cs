using UnityEngine;

namespace UnityLightsLodSystem.Runtime
{
    public static class TransformUtilities
    {
        /// <summary>
        /// A slow (CPU), but accurate way to get the distance between two transforms.
        /// </summary>
        /// <param name="firstGoTransform"></param> The transform of the first GameObject.
        /// <param name="secondGoTransform"></param> The transform of the second GameObject.
        /// <returns></returns> A floating number representing the distance between the two objects.
        public static float GetDistanceSlow(Transform firstGoTransform, Transform secondGoTransform)
        {
            return Vector3.Distance(firstGoTransform.position, secondGoTransform.position);
        }
        
        /// <summary>
        /// A less precise, but more CPU efficient way of getting the distance between two transforms.
        /// </summary>
        /// <param name="firstGoTransform"></param> The transform of the first GameObject.
        /// <param name="secondGoTransform"></param> The transform of the second GameObject.
        /// <returns></returns> A floating number representing the square distance between the two objects.
        public static float GetSquaredDistanceFast(Transform firstGoTransform, Transform secondGoTransform)
        {
            Vector3 vector = firstGoTransform.position - secondGoTransform.position;
            return Vector3.SqrMagnitude(vector);
        }
    }
}