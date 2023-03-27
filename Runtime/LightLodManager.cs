using UnityEngine;
using UnityEngine.Profiling;

namespace UnityLightsLodSystem.Runtime
{
    public class LightLodManager : MonoBehaviour
    {
        [Header("How often to update the lights (seconds)")]
        public float updateRate = 0.1f;

        private static LightLodManager _instance;
        
        private Camera _camera;
        private Transform _cameraTransform;
        private LightLod[] _lightLods;
        private float _timeElapsed;

        public static LightLodManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LightLodManager>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            
            _lightLods = FindObjectsOfType<LightLod>();
        }

        private void Start()
        {
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
            _timeElapsed = 0;
        }

        private void Update()
        {
            Profiler.BeginSample("UpdateLightOptimizations");
            
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed > updateRate)
            {
                UpdateLightOptimizations();
            }
            
            Profiler.EndSample();
        }

        private void UpdateLightOptimizations()
        {
            foreach (var lightLod in _lightLods)
            {
                lightLod.UpdateLightOptimizations(_camera, _cameraTransform);
            }

            _timeElapsed = 0;
        }

    }
}
