using UnityEngine;

namespace CodeBase.StaticData.Finish
{
    [CreateAssetMenu(menuName = "Static Data/Finish Static Data", order = 0)]
    public class FinishStaticData : ScriptableObject
    {
        [field: SerializeField] public FinishPoint.FinishPoint FinishPrefab { get; private set; }
    }
}