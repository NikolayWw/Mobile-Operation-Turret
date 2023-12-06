using System;
using UnityEngine;

namespace CodeBase.StaticData.Car
{
    [Serializable]
    public class TurretConfig
    {
        [field: SerializeField] public float TurretRotateSpeed { get; private set; } = 5;
        [field: SerializeField] public float IntensityRotate { get; private set; } = 0.5f;
        [field: SerializeField] public float IntervalShoot { get; private set; } = 1f;
        [field: SerializeField] public float LimitedYFrom { get; private set; } = -20;
        [field: SerializeField] public float LimitedYTo { get; private set; } = 20;
    }
}