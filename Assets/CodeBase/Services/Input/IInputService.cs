using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        float TurretRotateAxis { get; }
        void SetHorizontalMouseAxis(float value);
    }
}