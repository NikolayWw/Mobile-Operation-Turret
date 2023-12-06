using System;

namespace CodeBase.Services.GameObserver
{
    public class GameObserverService : IGameObserverService
    {
        public Action OnPlayerLose { get; set; }
        public Action OnPlayerWin { get; set; }
        public Action OnGameStart { get; set; }

        public void Cleanup()
        {
            OnPlayerLose = null;
            OnPlayerWin = null;
            OnGameStart = null;
        }

        public void SendGameStart() =>
            OnGameStart?.Invoke();

        public void SendPlayerLose() =>
            OnPlayerLose?.Invoke();

        public void SendPlayerWin() =>
            OnPlayerWin?.Invoke();
    }
}