using UI.Views;
using UnityEngine;

namespace ATG.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class LookAtCameraCanvas<T>: UIView<T>
    {
        [SerializeField] private float minDirectionMagnitude = 0.1f;
        [SerializeField] private bool ignoreY;
        
        private UnityEngine.Camera _camera;

        protected override void Awake()
        {
            base.Awake();
            
            _camera = UnityEngine.Camera.main;
            
            _canvas.renderMode = RenderMode.WorldSpace;
            _canvas.worldCamera = _camera;
        }

        private void Update()
        {
            if(_canvas.enabled == false) return;
            
            Vector3 direction = _camera.transform.position - _canvas.transform.position;
            direction.y /= 2f;
            
            if (ignoreY == true)
            {
                direction.y = 0f;
            }

            if (direction.sqrMagnitude > minDirectionMagnitude)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                _canvas.transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}