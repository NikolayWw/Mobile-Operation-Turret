using CodeBase.Bullet;
using CodeBase.Services.Factory;
using CodeBase.StaticData.Bullet;
using UnityEngine;

namespace CodeBase.Logic.Pool
{
    public class BulletPoolHandler : BaseObjectPoolHandler<BulletId, BulletObjectPool>
    {
        private readonly IGameFactory _gameFactory;

        public BulletPoolHandler(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Get(BulletId id, out BulletMove bulletMove)
        {
            PoolObjectData data = GetData(id);
            bulletMove = (BulletMove)data.ReciveComponent(typeof(BulletMove));
            data.ObjectPool.Enable();
        }

        protected override PoolObjectData NewObjectPoolData(BulletId key)
        {
            GameObject instance = _gameFactory.CreateBullet(key);
            BulletMove bulletMove = instance.GetComponent<BulletMove>();
            BulletObjectPool poolObject = instance.GetComponent<BulletObjectPool>();
            return new PoolObjectData(poolObject, bulletMove);
        }
    }
}