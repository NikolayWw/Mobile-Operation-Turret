using CodeBase.StaticData.Bullet;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreatePlayer(Vector3 at);
        GameObject CreateBullet(BulletId id);
        void CreateEnemy(EnemyId id, Vector3 at);
        void CreateFinish(Vector3 at);
        GameObject Car { get; }
        void InitGameScene(UnityEngine.Camera mainCamera);
    }
}