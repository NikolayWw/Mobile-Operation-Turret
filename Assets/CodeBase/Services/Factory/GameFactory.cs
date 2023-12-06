using CodeBase.Bullet;
using CodeBase.Camera;
using CodeBase.Enemy;
using CodeBase.Player.Car;
using CodeBase.Player.Turret;
using CodeBase.Services.GameObserver;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Bullet;
using CodeBase.StaticData.Enemy;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly AllServices _allServices;
        public GameObject Car { get; private set; }

        private UnityEngine.Camera _mainCamera;
        private Transform _enemyContainer;
        private Transform _bulletContainer;

        public GameFactory(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void InitGameScene(UnityEngine.Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _enemyContainer = new GameObject("Enemy Container").transform;
            _bulletContainer = new GameObject("Bullet Container").transform;
        }

        public void CreatePlayer(Vector3 at)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IInputService inputService = GetService<IInputService>();
            IUIFactory uiFactory = GetService<IUIFactory>();
            IGameObserverService observerService = GetService<IGameObserverService>();
            ILogicFactory logicFactory = GetService<ILogicFactory>();

            GameObject cameraBrainPrefab = dataService.CarStaticData.CameraConfig.CameraBrainPrefab;
            GameObject carPrefab = dataService.CarStaticData.CarConfig.Prefab;

            GameObject carInstance = Object.Instantiate(carPrefab, at, Quaternion.identity);
            carInstance.GetComponent<CarMove>().Construct(dataService, observerService);
            carInstance.GetComponent<CarHealth>().Construct(dataService, uiFactory, observerService);

            carInstance.GetComponent<TurretShoot>().Construct(this, dataService, observerService, logicFactory);
            carInstance.GetComponent<TurretLook>().Construct(inputService, dataService, observerService);

            GameObject cameraBrainInstance = Object.Instantiate(cameraBrainPrefab);
            cameraBrainInstance.GetComponent<CameraAnimation>().Construct(_mainCamera, observerService);
            cameraBrainInstance.GetComponent<CameraFollow>().Construct(carInstance.transform);

            Car = carInstance;
        }

        public void CreateEnemy(EnemyId id, Vector3 at)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameObserverService gameObserverService = GetService<IGameObserverService>();

            EnemyConfig config = dataService.ForEnemy(id);

            GameObject instantiate = Object.Instantiate(config.Prefab, at, Quaternion.Euler(0, Random.Range(-360, 360), 0));
            instantiate.GetComponent<EnemyMove>().Construct(config, gameObserverService);
            instantiate.GetComponent<EnemyAttack>().Construct(config, gameObserverService);
            instantiate.GetComponent<EnemyAnimation>().Construct(gameObserverService);
            instantiate.GetComponentInChildren<EnemyFindTargetReporter>().Construct(config.FindTargetRadius);
            instantiate.transform.SetParent(_enemyContainer);
        }

        public void CreateFinish(Vector3 at)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IUIFactory uiFactory = GetService<IUIFactory>();
            IGameObserverService observerService = GetService<IGameObserverService>();

            FinishPoint.FinishPoint prefab = dataService.FinishStaticData.FinishPrefab;
            FinishPoint.FinishPoint instantiate = Object.Instantiate(prefab, at, Quaternion.identity);
            instantiate.Construct(uiFactory, observerService);
        }

        public GameObject CreateBullet(BulletId id)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            BulletConfig config = dataService.ForBullet(id);

            GameObject instance = Object.Instantiate(config.Prefab, _bulletContainer);
            instance.GetComponent<BulletObjectPool>().Construct(dataService);
            return instance;
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}