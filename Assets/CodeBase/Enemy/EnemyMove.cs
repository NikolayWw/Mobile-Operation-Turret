using CodeBase.Services.GameObserver;
using CodeBase.StaticData.Enemy;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyFindTargetReporter _findTarget;

        private EnemyConfig _config;
        private IGameObserverService _observerService;

        public void Construct(EnemyConfig config, IGameObserverService observerService)
        {
            _config = config;
            _observerService = observerService;

            _findTarget.OnCarEnter += StartFollowTarget;
            _observerService.OnPlayerWin += Freeze;
            _observerService.OnPlayerLose += Freeze;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            _observerService.OnPlayerWin -= Freeze;
            _observerService.OnPlayerLose -= Freeze;
        }

        private void StartFollowTarget()
        {
            _agent.speed = _config.Speed;
            StartCoroutine(FollowTarget());
        }

        private IEnumerator FollowTarget()
        {
            WaitForSeconds wait = new WaitForSeconds(0.4f);//delay
            while (true)
            {
                if (_agent.isActiveAndEnabled == false)
                    _agent.enabled = true;

                _agent.SetDestination(_findTarget.CarTransform.position);
                yield return wait;
            }
        }

        private void Freeze()
        {
            _findTarget.OnCarEnter -= StartFollowTarget;
            StopAllCoroutines();
            _agent.stoppingDistance = 2f;//stop move
            _agent.SetDestination(transform.position);
            _agent.speed = 0;
            _agent.velocity *= 0;
        }
    }
}