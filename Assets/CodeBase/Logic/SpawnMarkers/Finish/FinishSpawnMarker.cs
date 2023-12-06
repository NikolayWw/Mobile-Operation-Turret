using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers.Finish
{
    public class FinishSpawnMarker : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, 3f);
        }
    }
}