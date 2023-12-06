using CodeBase.Logic.Pool;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Bullet;

namespace CodeBase.Services.LogicFactory
{
    public class LogicFactory : ILogicFactory
    {
        private readonly AllServices _services;
        public EnemySpawner.EnemySpawner EnemySpawner { get; private set; }
        public BulletPoolHandler BulletPoolHandler { get; private set; }

        public LogicFactory(AllServices services)
        {
            _services = services;
        }

        public void Cleanup()
        {
            EnemySpawner = null;
            BulletPoolHandler = null;
        }

        public void InitializeBulletPoolHandler(int count, BulletId[] ids)
        {
            IGameFactory gameFactory = GetService<IGameFactory>();
            BulletPoolHandler = new BulletPoolHandler(gameFactory);
            BulletPoolHandler.InitStartObjects(count, ids);
        }

        public void InitializeEnemySpawner()
        {
            IGameFactory gameFactory = GetService<IGameFactory>();
            IStaticDataService dataService = GetService<IStaticDataService>();

            EnemySpawner = new EnemySpawner.EnemySpawner(gameFactory, dataService);
        }

        private TService GetService<TService>() where TService : IService =>
            _services.Single<TService>();
    }
}