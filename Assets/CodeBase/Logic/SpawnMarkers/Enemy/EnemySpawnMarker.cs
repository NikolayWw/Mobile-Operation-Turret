using System.Collections.Generic;
using CodeBase.StaticData.Enemy.SpawnData;
using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers.Enemy
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        public List<EnemySpawnConfig> Configs;
        [field: SerializeField] public int RandomFrom { get; private set; }
        [field: SerializeField] public int RandomTo { get; private set; }
        [field: SerializeField] public Transform PointFrom { get; private set; }
        [field: SerializeField] public Transform PointTo { get; private set; }

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 2f);
            if (PointFrom == null || PointTo == null)
                return;

            Vector3 toRight = new Vector3(PointTo.position.x, PointTo.position.y, PointFrom.position.z);
            Vector3 toDown = new Vector3(PointTo.position.x, PointTo.position.y, PointTo.position.z);
            Vector3 toLeft = new Vector3(PointFrom.position.x, PointTo.position.y, PointTo.position.z);

            Gizmos.DrawLine(PointFrom.position, toRight);
            Gizmos.DrawLine(toRight, toDown);
            Gizmos.DrawLine(toDown, toLeft);
            Gizmos.DrawLine(toLeft, PointFrom.position);
        }
    }
}