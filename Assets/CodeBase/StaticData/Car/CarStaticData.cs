using UnityEngine;

namespace CodeBase.StaticData.Car
{
    [CreateAssetMenu(menuName = "Static Data/Car Static Data", order = 0)]
    public class CarStaticData : ScriptableObject
    {
        public CarConfig CarConfig;
        public TurretConfig TurretConfig;
        public CameraConfig CameraConfig;
    }
}