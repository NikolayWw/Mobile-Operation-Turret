using System;
using UnityEngine;

namespace CodeBase.StaticData.Enemy.SpawnData
{
    [Serializable]
    public class EnemySpawnConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public EnemyId Id { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}