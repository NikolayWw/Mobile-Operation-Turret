using CodeBase.StaticData.Bullet;
using CodeBase.StaticData.Car;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Finish;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Windows;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();

        WindowStaticData WindowData { get; }
        CarStaticData CarStaticData { get; }
        BulletStaticData BulletStaticData { get; }
        EnemyStaticData EnemyStaticData { get; }
        LevelStaticData LevelData { get; }
        FinishStaticData FinishStaticData { get; }

        BulletConfig ForBullet(BulletId id);

        EnemyConfig ForEnemy(EnemyId id);
    }
}