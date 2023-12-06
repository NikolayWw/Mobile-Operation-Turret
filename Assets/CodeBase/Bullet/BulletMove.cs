using UnityEngine;

namespace CodeBase.Bullet
{
    public class BulletMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _mainTransform;

        public void Move(Vector3 position, Vector3 direction, float force)
        {
            _mainTransform.position = position;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
        }
    }
}