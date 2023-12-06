using System;
using UnityEngine;

namespace CodeBase.StaticData.Car
{
    [Serializable]
    public class CameraConfig
    {
        [field: SerializeField] public GameObject CameraBrainPrefab { get; private set; }
    }
}