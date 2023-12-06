using System;
using UnityEngine;

namespace CodeBase.StaticData.Bullet
{
    [Serializable]
    public class BulletConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public BulletId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}