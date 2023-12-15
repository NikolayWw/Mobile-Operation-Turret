using CodeBase.Data;
using CodeBase.Services;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
    public class WarmupState : IState
    {
        private readonly AllServices _allServices;

        public WarmupState(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void Enter()
        {
            Warmup();
            LoadLevel();
        }

        public void Exit()
        { }

        private void LoadLevel()
        {
            IGameStateMachine gameStateMachine = GetService<IGameStateMachine>();
            gameStateMachine.Enter<LoadLevelState, string>(GameConstants.MainSceneKey);
        }

        private void Warmup()
        {
            IUIFactory uiFactory = GetService<IUIFactory>();
            uiFactory.CreateWinWindow();
            uiFactory.CreateLoseWindow();
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}