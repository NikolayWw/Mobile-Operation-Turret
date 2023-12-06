using CodeBase.Logic;
using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IApplyDamage
    {
        public Action OnHappened;

        public void ApplyDamage(float value)
        {
            OnHappened?.Invoke();
            Destroy(gameObject);
        }
    }
}