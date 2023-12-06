using CodeBase.Services.GameObserver;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.StartGame
{
    public class StartGameWindow : MonoBehaviour, IPointerClickHandler
    {
        private IGameObserverService _gameObserver;
        private bool _isClicked;

        public void Construct(IGameObserverService gameObserverService)
        {
            _gameObserver = gameObserverService;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isClicked)
                return;

            _isClicked = true;
            _gameObserver.SendGameStart();
            Destroy(gameObject);
        }
    }
}