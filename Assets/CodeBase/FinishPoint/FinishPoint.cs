using CodeBase.Services.GameObserver;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.FinishPoint
{
    public class FinishPoint : MonoBehaviour
    {
        [SerializeField] private FinishFindCar _triggerReporter;

        private IUIFactory _uiFactory;
        private IGameObserverService _gameObserverService;

        public void Construct(IUIFactory uiFactory, IGameObserverService gameObserverService)
        {
            _uiFactory = uiFactory;
            _gameObserverService = gameObserverService;
            _triggerReporter.OnCarEnter += SendFinish;
        }

        private void SendFinish()
        {
            _uiFactory.CreateWinWindow();
            _gameObserverService.SendPlayerWin();
        }
    }
}