using CodeBase.Logic;
using CodeBase.Player.Car;
using UnityEngine;

namespace CodeBase.Bullet
{
    public class BulletApplyDamage : MonoBehaviour
    {
        private bool _triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered)
                return;

            if (other.isTrigger)
                return;

            Rigidbody attachedRigidbody = other.attachedRigidbody;
            if (attachedRigidbody != null
                && attachedRigidbody.TryGetComponent(out IApplyDamage applyDamage)
                && applyDamage.GetType() != typeof(CarHealth))
            {
                applyDamage.ApplyDamage(0);
                _triggered = true;
            }
        }

        private void OnDisable()
        {
            _triggered = false;
        }
    }
}