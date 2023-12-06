using CodeBase.Services.GameObserver;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Car;
using UnityEngine;

namespace CodeBase.Player.Car
{
    public class CarMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private CarConfig _config;
        private IGameObserverService _gameObserverService;

        public void Construct(IStaticDataService dataService, IGameObserverService gameObserverService)
        {
            _config = dataService.CarStaticData.CarConfig;
            _gameObserverService = gameObserverService;

            _gameObserverService.OnGameStart += StartMove;
            _gameObserverService.OnPlayerLose += Freeze;
            _gameObserverService.OnPlayerWin += Freeze;
        }

        private void Start()
        {
            Freeze();
        }

        private void OnDestroy()
        {
            _gameObserverService.OnGameStart -= StartMove;
            _gameObserverService.OnPlayerLose -= Freeze;
            _gameObserverService.OnPlayerWin -= Freeze;
        }

        private void FixedUpdate()
        {
            UpdateMove();
        }

        private void UpdateMove()
        {
            _rigidbody.velocity = _config.ForwardSpeed * 20 * Time.fixedDeltaTime * transform.forward;//extra speed
        }

        private void Freeze()
        {
            enabled = false;
        }

        private void StartMove()
        {
            enabled = true;
        }
    }
}