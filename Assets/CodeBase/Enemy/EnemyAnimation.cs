using CodeBase.Services.GameObserver;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        private readonly int RunningHash = Animator.StringToHash("Running");

        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        private IGameObserverService _observerService;

        public void Construct(IGameObserverService observerService)
        {
            _observerService = observerService;

            _observerService.OnPlayerLose += Freeze;
            _observerService.OnPlayerWin += Freeze;
        }

        private void OnDestroy()
        {
            _observerService.OnPlayerLose -= Freeze;
            _observerService.OnPlayerWin -= Freeze;
        }

        private void Update()
        {
            UpdateMove();
        }

        private void UpdateMove() =>
            _animator.SetBool(RunningHash, _agent.velocity.magnitude > 0.4f); //trigger magnitude

        private void Freeze()
        {
            enabled = false;
            _animator.SetBool(RunningHash, false);
        }
    }
}