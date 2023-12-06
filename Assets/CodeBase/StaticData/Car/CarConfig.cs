using System;
using UnityEngine;

namespace CodeBase.StaticData.Car
{
    [Serializable]
    public class CarConfig
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float ForwardSpeed { get; private set; } = 5;
        [field: SerializeField] public float StartHealth { get; private set; } = 10;
    }
}