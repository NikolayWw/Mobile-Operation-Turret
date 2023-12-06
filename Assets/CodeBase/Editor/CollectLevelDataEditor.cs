using CodeBase.Logic.SpawnMarkers;
using CodeBase.Logic.SpawnMarkers.Enemy;
using CodeBase.Logic.SpawnMarkers.Finish;
using CodeBase.StaticData.Enemy.SpawnData;
using CodeBase.StaticData.Level;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class CollectLevelDataEditor
    {
        private const string LevelStaticDataPath = "Level/LevelStaticData";

        [MenuItem("Tools/Collect Level Data")]
        private static void CollectLevelData()
        {
            LevelStaticData levelData = Resources.Load<LevelStaticData>(LevelStaticDataPath);
            Vector3 playerInitialPoint = Object.FindObjectOfType<PlayerSpawnMarker>().transform.position;
            Vector3 finishPoint = Object.FindObjectOfType<FinishSpawnMarker>().transform.position;

            levelData.SetData(EnemySpawnData(), playerInitialPoint, finishPoint);

            EditorUtility.SetDirty(levelData);
        }

        private static EnemySpawnData EnemySpawnData()
        {
            EnemySpawnMarker marker = Object.FindObjectOfType<EnemySpawnMarker>();
            EnemySpawnData enemyData = new EnemySpawnData(marker.Configs, marker.RandomFrom, marker.RandomTo,
                marker.PointFrom.position, marker.PointTo.position, marker.transform.position.y);
            return enemyData;
        }
    }
}