using CodeBase.Bullet;
using CodeBase.Services.Factory;
using CodeBase.Services.GameObserver;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Bullet;
using CodeBase.StaticData.Car;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Player.Turret
{
    public class TurretShoot : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;

        private TurretConfig _config;
        private IGameFactory _gameFactory;
        private IStaticDataService _dataService;
        private IGameObserverService _observerService;
        private ILogicFactory _logicFactory;

        public void Construct(IGameFactory gameFactory, IStaticDataService dataService, IGameObserverService observerService, ILogicFactory logicFactory)
        {
            _gameFactory = gameFactory;
            _dataService = dataService;
            _config = dataService.CarStaticData.TurretConfig;
            _observerService = observerService;
            _logicFactory = logicFactory;

            _observerService.OnGameStart += StartShoot;
            _observerService.OnPlayerLose += Freeze;
            _observerService.OnPlayerWin += Freeze;
        }

        private void Start()
        {
            InitPool();
        }

        private void OnDestroy()
        {
            _observerService.OnGameStart -= StartShoot;
            _observerService.OnPlayerLose -= Freeze;
            _observerService.OnPlayerWin -= Freeze;
        }

        private void InitPool()
        {
            BulletId[] ids = _dataService.BulletStaticData.Configs.Select(x => x.Id).ToArray();
            _logicFactory.InitializeBulletPoolHandler(70, ids);//pool count
        }

        private IEnumerator Shoot()
        {
            WaitForSeconds wait = new(_config.IntervalShoot);
            List<BulletConfig> bulletConfigs = _dataService.BulletStaticData.Configs;

            while (true)
            {
                yield return wait;

                int bulletIndex = Random.Range(0, bulletConfigs.Count);
                BulletId bulletId = bulletConfigs[bulletIndex].Id;
                _logicFactory.BulletPoolHandler.Get(bulletId, out BulletMove move);
                move.Move(_shootPoint.position, _shootPoint.forward, _dataService.BulletStaticData.Force);
            }
        }

        private void StartShoot()
        {
            StartCoroutine(Shoot());
        }

        private void Freeze()
        {
            StopAllCoroutines();
        }
    }
}