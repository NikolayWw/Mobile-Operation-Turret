using System;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [Serializable]
    public class EnemyConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public EnemyId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float Damage { get; private set; } = 1f;
        [field: SerializeField] public float FindTargetRadius { get; private set; } = 5f;

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}