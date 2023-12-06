using CodeBase.StaticData.Enemy.SpawnData;
using UnityEngine;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level Static Data", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public Vector3 PlayerInitialPoint { get; private set; }
        [field: SerializeField] public Vector3 FinishPointPosition { get; private set; }

        public EnemySpawnData EnemySpawnData;

        public void SetData(EnemySpawnData enemySpawn, Vector3 playerInitialPoint, Vector3 finishPoint)
        {
            PlayerInitialPoint = playerInitialPoint;
            EnemySpawnData = enemySpawn;
            FinishPointPosition = finishPoint;
            
            EnemySpawnData.OnValidate();
        }

        private void OnValidate()
        {
            EnemySpawnData.OnValidate();
        }
    }
}