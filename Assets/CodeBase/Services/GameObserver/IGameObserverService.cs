using System;

namespace CodeBase.Services.GameObserver
{
    public interface IGameObserverService : IService
    {
        Action OnPlayerLose { get; set; }
        Action OnPlayerWin { get; set; }
        Action OnGameStart { get; set; }

        void Cleanup();

        void SendPlayerLose();

        void SendPlayerWin();
        void SendGameStart();
    }
}