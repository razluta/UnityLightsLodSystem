using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityLightsLodSystem.Runtime
{
    [RequireComponent(typeof(Light))]
    public class LightLod : MonoBehaviour
    {
        [SerializeField] public LightLevelOfDetail[] lightLods;

        private Light _light;
        private float _lightRangeOfInfluence;
        private Transform _transform;
        private int _lodCount;
        private bool _isLightCulled;

        public void UpdateLightOptimizations(Camera activeCamera, Transform cameraTransform)
        {
            UpdateLightCulling(activeCamera);
            if (!_isLightCulled)
            {
                UpdateLightParametersBasedOnLod(cameraTransform);
            }
        }

        private void UpdateLightCulling(Camera activeCamera)
        {
            // Frustum based light culling
            Bounds lightBounds = new Bounds(_transform.position, Vector3.one * (_lightRangeOfInfluence * 2f));
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(activeCamera);
            _isLightCulled = !GeometryUtility.TestPlanesAABB(frustumPlanes, lightBounds);
            _light.enabled = !_isLightCulled;
        }

        private void UpdateLightParametersBasedOnLod(Transform cameraTransform)
        {
            var sqrDistance = TransformUtilities.GetSquaredDistanceFast(cameraTransform, _transform);

            // No light changes if the distance is less than determined by the LOD1
            if (sqrDistance < lightLods[0].MinSqrDistance)
            {
                _light.enabled = true;
                return;
            }
            
            // Traversing and checking the LODs 1-X
            for (int i = 0; i < _lodCount; i++)
            {
                if((sqrDistance >= lightLods[i].MinSqrDistance) && (sqrDistance < lightLods[i].MaxSqrDistance))
                {
                    _light.enabled = true;
                    _light.shadows = lightLods[i].shadowType;
                    _light.shadowResolution = lightLods[i].shadowResolution;
                    
#if UNITY_EDITOR
                    // Change the color for debug purposes in the Unity Editor
                    _light.color = lightLods[i].debugColor;
#endif
                }
            }
            
            // Disable light if distance higher than the last LOD's max
            if (sqrDistance >= lightLods[_lodCount - 1].MaxSqrDistance)
            {
                _light.enabled = false;
            }
        }

        private void Start()
        {
            _light = GetComponent<Light>();
            _transform = _light.transform;
            _isLightCulled = false;

            foreach (var lightLevelOfDetail in lightLods)
            {
                lightLevelOfDetail.MinSqrDistance = lightLevelOfDetail.minDistance * lightLevelOfDetail.minDistance;
                lightLevelOfDetail.MaxSqrDistance = lightLevelOfDetail.maxDistance * lightLevelOfDetail.maxDistance;
            }

            _lodCount = lightLods.Length;

            switch (_light.type)
            {
                case LightType.Point:
                {
                    _lightRangeOfInfluence = _light.range;
                }
                    break;
                
                case LightType.Spot:
                {
                    _lightRangeOfInfluence = _light.range;
                }
                    break;
            }
        }
    }

    [Serializable]
    public class LightLevelOfDetail
    {
        [SerializeField] public float minDistance;
        [SerializeField] public float maxDistance;
        [SerializeField] public LightShadows shadowType;
        [SerializeField] public LightShadowResolution shadowResolution = LightShadowResolution.High;
        [SerializeField] public Color debugColor;

        public float MinSqrDistance { get; set; }
        public float MaxSqrDistance { get; set; }
    }
}