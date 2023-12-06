using CodeBase.Data;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.Win
{
    public class WinWindow : MonoBehaviour, IPointerClickHandler
    {
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public void OnPointerClick(PointerEventData eventData) =>
            _gameStateMachine.Enter<LoadLevelState, string>(GameConstants.MainSceneKey);
    }
}