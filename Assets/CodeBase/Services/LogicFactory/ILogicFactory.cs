using CodeBase.Logic.Pool;
using CodeBase.StaticData.Bullet;

namespace CodeBase.Services.LogicFactory
{
    public interface ILogicFactory : IService
    {
        EnemySpawner.EnemySpawner EnemySpawner { get; }
        BulletPoolHandler BulletPoolHandler { get; }

        void InitializeEnemySpawner();

        void Cleanup();
        void InitializeBulletPoolHandler(int count, BulletId[] ids);
    }
}