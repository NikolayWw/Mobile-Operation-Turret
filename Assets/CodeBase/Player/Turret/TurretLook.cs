using CodeBase.Services.GameObserver;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Car;
using UnityEngine;

namespace CodeBase.Player.Turret
{
    public class TurretLook : MonoBehaviour
    {
        [SerializeField] private Transform _turretBody;

        private IInputService _inputService;
        private TurretConfig _config;

        private float _xRotation;
        private float _yRotation;
        private IGameObserverService _observerService;

        public void Construct(IInputService inputService, IStaticDataService dataService, IGameObserverService observerService)
        {
            _inputService = inputService;
            _config = dataService.CarStaticData.TurretConfig;
            _xRotation = _turretBody.eulerAngles.x;

            _observerService = observerService;
            _observerService.OnGameStart += StartLook;
            _observerService.OnPlayerWin += Freeze;
            _observerService.OnPlayerLose += Freeze;
        }

        private void Start()
        {
            Freeze();
        }

        private void OnDestroy()
        {
            _observerService.OnGameStart -= StartLook;
            _observerService.OnPlayerWin -= Freeze;
            _observerService.OnPlayerLose -= Freeze;
        }

        private void Update()
        {
            UpdateLook();
        }

        private void UpdateLook()
        {
            _yRotation += _inputService.TurretRotateAxis * _config.IntensityRotate;
            _yRotation = Mathf.Clamp(_yRotation, _config.LimitedYFrom, _config.LimitedYTo);
            _turretBody.localRotation = Quaternion.Euler(_xRotation, 0f, _yRotation);
        }

        private void Freeze()
        {
            enabled = false;
        }

        private void StartLook()
        {
            enabled = true;
        }
    }
}