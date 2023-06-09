﻿using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityLightsLodSystem.Runtime
{
    [RequireComponent(typeof(Light))]
    public class LightLod : MonoBehaviour
    {
        [SerializeField] public bool isDisableAfterLastLod = true;
        [SerializeField] public float minDistanceFirstLod;
        [SerializeField] public LightLevelOfDetail[] lightLods;

        private Light _light;
        private float _lightRangeOfInfluence;
        private Transform _transform;
        private int _lodCount;
        private bool _isLightCulled;
        private float _minDistanceSqrFirstLod;
        private LightLevelOfDetail _cachedLightSettings;

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
            if (sqrDistance < _minDistanceSqrFirstLod)
            {
                _light.enabled = true;
                _light.shadows = _cachedLightSettings.shadowType;
                _light.shadowResolution = _cachedLightSettings.shadowResolution;
                    
#if UNITY_EDITOR
                // Change the color for debug purposes in the Unity Editor only
                _light.color = _cachedLightSettings.debugColor;
#endif
                
                return;
            }

            var previousLodMaxSquareDistance = _minDistanceSqrFirstLod;
            // Traversing and checking the LODs 1-X
            for (int i = 0; i < _lodCount; i++)
            {
                if (i > 0)
                {
                    previousLodMaxSquareDistance = lightLods[i - 1].MaxSqrDistance;
                }
                
                if((sqrDistance >= previousLodMaxSquareDistance) && (sqrDistance < lightLods[i].MaxSqrDistance))
                {
                    _light.enabled = true;
                    _light.shadows = lightLods[i].shadowType;
                    _light.shadowResolution = lightLods[i].shadowResolution;
                    
#if UNITY_EDITOR
                    // Change the color for debug purposes in the Unity Editor only
                    _light.color = lightLods[i].debugColor;
#endif
                }
            }
            
            // Disable light if distance higher than the last LOD's max
            if (sqrDistance >= lightLods[_lodCount - 1].MaxSqrDistance && isDisableAfterLastLod)
            {
                _light.enabled = false;
            }
        }

        private void Start()
        {
            InitializeLightLodData();
        }

        private void InitializeLightLodData()
        {
            _light = GetComponent<Light>();
            _transform = _light.transform;
            _isLightCulled = false;

            _minDistanceSqrFirstLod = minDistanceFirstLod * minDistanceFirstLod;
            foreach (var lightLevelOfDetail in lightLods)
            {
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

            _cachedLightSettings = new LightLevelOfDetail()
            {
                maxDistance = minDistanceFirstLod,
                shadowType = _light.shadows,
                shadowResolution = _light.shadowResolution,
                debugColor = _light.color
            };
        }

        private void OnDrawGizmos()
        {
            if (_light == null)
            {
                InitializeLightLodData();
            }

            if (_light.enabled == false) return;

            var lightColor = _light.color;
            Gizmos.color = new Color(lightColor.r, lightColor.g, lightColor.b, 0.3f);
            Gizmos.DrawSphere(_transform.position, _lightRangeOfInfluence);
        }
    }

    [Serializable]
    public class LightLevelOfDetail
    {
        [SerializeField] public float maxDistance;
        [SerializeField] public LightShadows shadowType;
        [SerializeField] public LightShadowResolution shadowResolution = LightShadowResolution.High;
        [SerializeField] public Color debugColor;
        
        public float MaxSqrDistance { get; set; }
    }
}