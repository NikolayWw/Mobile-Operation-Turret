using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.GameObserver;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitSceneKey, OnLoaded);
        }

        public void Exit()
        { }

        private void RegisterServices()
        {
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterStaticData();
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services));
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services));
            _services.RegisterSingle<ILogicFactory>(new LogicFactory(_services));
            _services.RegisterSingle<IGameObserverService>(new GameObserverService());
            _services.RegisterSingle<IInputService>(new InputService());
            _services.RegisterSingle<ICleanupService>(new CleanupService(_services.Single<ILogicFactory>(), _services.Single<IGameObserverService>()));
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<LoadLevelState, string>(MainSceneKey);
        }

        private void RegisterStaticData()
        {
            var service = new StaticDataService();
            service.Load();
            _services.RegisterSingle<IStaticDataService>(service);
        }
    }
}