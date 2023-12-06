using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Bullet
{
    [CreateAssetMenu(menuName = "Static Data/Bullet Static Data", order = 0)]
    public class BulletStaticData : ScriptableObject
    {
        [field: SerializeField] public float LifeTime { get; private set; } = 10f;
        [field: SerializeField] public float Force { get; private set; } = 15f;
        public List<BulletConfig> Configs;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}