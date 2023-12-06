namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        public float TurretRotateAxis { get; private set; }

        public void SetHorizontalMouseAxis(float horizontal)
        {
            TurretRotateAxis = horizontal;
        }
    }
}