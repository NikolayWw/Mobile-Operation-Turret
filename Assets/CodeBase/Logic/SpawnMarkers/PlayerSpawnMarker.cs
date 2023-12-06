using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers
{
    public class PlayerSpawnMarker : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 2f);
        }
    }
}