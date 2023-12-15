using CodeBase.Player.Car;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.HUD
{
    public class CarHealthWindow : MonoBehaviour
    {
        [SerializeField] private Image _healthFillImage;
        private CarHealth _carHealth;
        private float _startHealth;

        public void Construct(IGameFactory gameFactory, IStaticDataService dataService)
        {
            _startHealth = dataService.CarStaticData.CarConfig.StartHealth;
            _carHealth = gameFactory.Car.GetComponent<CarHealth>();

            _carHealth.OnValueChange += Refresh;
        }

        private void Start()
        {
            Refresh();
        }

        private void Refresh() =>
            _healthFillImage.fillAmount = _carHealth.CurrentHealth / _startHealth;
    }
}