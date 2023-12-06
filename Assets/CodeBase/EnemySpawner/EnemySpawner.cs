using System.Collections.Generic;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Enemy.SpawnData;
using UnityEngine;

namespace CodeBase.EnemySpawner
{
    public class EnemySpawner
    {
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _dataService;

        public EnemySpawner(IGameFactory gameFactory, IStaticDataService dataService)
        {
            _gameFactory = gameFactory;
            _dataService = dataService;
        }

        public void Spawn()
        {
            List<EnemyConfig> enemyConfigs = _dataService.EnemyStaticData.Configs;
            EnemySpawnData spawnData = _dataService.LevelData.EnemySpawnData;

            float xLenght = (spawnData.PositionFrom.x - spawnData.PositionTo.x) / 2;
            float zLenght = (spawnData.PositionFrom.z - spawnData.PositionTo.z) / 2;

            float positionX = spawnData.PositionFrom.x - xLenght;
            float positionZ = spawnData.PositionTo.z + zLenght;

            int enemyCount = Random.Range(spawnData.RandomFrom, spawnData.RandomFrom);

            for (int i = 0; i < enemyCount; i++)
            {
                float x = Random.Range(positionX - xLenght, positionX + xLenght);
                float z = Random.Range(positionZ - zLenght, positionZ + zLenght);

                Vector3 position = new Vector3(x, spawnData.Height, z);
                EnemyConfig config = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

                _gameFactory.CreateEnemy(config.Id, position);
            }
        }
    }
}