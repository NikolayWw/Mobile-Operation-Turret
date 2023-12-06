using CodeBase.Logic;
using CodeBase.Player.Car;
using UnityEngine;

namespace CodeBase.Bullet
{
    public class BulletApplyDamage : MonoBehaviour
    {
        [SerializeField] private BulletObjectPool _bulletObjectPool;

        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger)
                return;

            Rigidbody attachedRigidbody = other.attachedRigidbody;
            if (attachedRigidbody != null
                && attachedRigidbody.TryGetComponent(out IApplyDamage applyDamage)
                && applyDamage.GetType() != typeof(CarHealth))
            {
                applyDamage.ApplyDamage(0);
                _bulletObjectPool.Disable();
            }
        }
    }
}