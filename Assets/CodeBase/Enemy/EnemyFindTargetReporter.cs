using System;
using CodeBase.Player.Car;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyFindTargetReporter : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;

        public Action OnCarEnter;
        public Transform CarTransform { get; private set; }
        public CarHealth CarHealth { get; private set; }

        public void Construct(float triggerRadius)
        {
            _collider.radius = triggerRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody attachedRigidbody = other.attachedRigidbody;
            if (attachedRigidbody != null && attachedRigidbody.TryGetComponent(out CarHealth health))
            {
                CarHealth = health;
                CarTransform = attachedRigidbody.transform;
                OnCarEnter?.Invoke();
            }
        }
    }
}