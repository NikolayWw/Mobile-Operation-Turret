using UnityEngine;

namespace CodeBase.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        private Transform _target;

        public void Construct(Transform target)
        {
            _target = target;
            UpdatePosition();
        }

        private void LateUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition() =>
            transform.position = _target.position + _offset;
    }
}