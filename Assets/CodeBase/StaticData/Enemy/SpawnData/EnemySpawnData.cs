using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Enemy.SpawnData
{
    [Serializable]
    public class EnemySpawnData
    {
        public List<EnemySpawnConfig> SpawnConfigs;

        [field: SerializeField] public int RandomFrom { get; private set; }
        [field: SerializeField] public int RandomTo { get; private set; }
        [field: SerializeField] public Vector3 PositionFrom { get; private set; }
        [field: SerializeField] public Vector3 PositionTo { get; private set; }
        [field: SerializeField] public float Height { get; private set; }

        public EnemySpawnData(List<EnemySpawnConfig> spawnConfigs, int randomFrom, int randomTo, Vector3 positionFrom, Vector3 positionTo, float height)
        {
            SpawnConfigs = spawnConfigs;
            RandomFrom = randomFrom;
            RandomTo = randomTo;
            PositionFrom = positionFrom;
            PositionTo = positionTo;
            Height = height;
        }

        public void OnValidate()
        {
            SpawnConfigs.ForEach(x => x.OnValidate());
        }
    }
}