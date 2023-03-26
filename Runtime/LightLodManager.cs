using UnityEngine;
using UnityEngine.Profiling;

namespace UnityLightsLodSystem.Runtime
{
    public class LightLodManager : MonoBehaviour
    {
        private Camera _camera;
        private Transform _cameraTransform;
        private LightLod[] _lightLods;

        private void Awake()
        {
            _lightLods = FindObjectsOfType<LightLod>();
        }

        private void Start()
        {
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
        }

        private void Update()
        {
            UpdateLightOptimizations();
        }

        private void UpdateLightOptimizations()
        {
            Profiler.BeginSample("UpdateLightOptimizations");
            
            foreach (var lightLod in _lightLods)
            {
                lightLod.UpdateLightOptimizations(_camera, _cameraTransform);
            }
            
            Profiler.EndSample();
        }

    }
}
