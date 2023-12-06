using CodeBase.Services.GameObserver;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyFindTargetReporter _findTargetReporter;
        [SerializeField] private EnemyCarCloseReporter _carCloseReporter;
        [SerializeField] private EnemyHealth _enemyHealth;

        private EnemyConfig _config;
        private IGameObserverService _observerService;

        public void Construct(EnemyConfig config, IGameObserverService observerService)
        {
            _config = config;
            _observerService = observerService;

            _observerService.OnPlayerLose += Freeze;
            _observerService.OnPlayerWin += Freeze;
            _carCloseReporter.OnCarEnter += ApplyAndSetDamage;
        }

        private void OnDestroy()
        {
            _observerService.OnPlayerLose -= Freeze;
            _observerService.OnPlayerWin -= Freeze;
        }

        private void Freeze()
        {
            _carCloseReporter.OnCarEnter -= ApplyAndSetDamage;
        }

        private void ApplyAndSetDamage()
        {
            _findTargetReporter.CarHealth.ApplyDamage(_config.Damage);
            _enemyHealth.ApplyDamage(0);
        }
    }
}