namespace CodeBase.Logic.Pool
{
    public interface IObjectPool
    {
        bool IsReady();

        void Enable();

        void Disable();
    }
}