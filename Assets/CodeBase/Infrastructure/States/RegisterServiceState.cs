using CodeBase.Services;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.GameObserver;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
    public class RegisterServiceState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly AllServices _services;

        public RegisterServiceState(IGameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;
        }

        public void Enter()
        {
            RegisterServices();
            _stateMachine.Enter<WarmupState>();
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

        private void RegisterStaticData()
        {
            var service = new StaticDataService();
            service.Load();
            _services.RegisterSingle<IStaticDataService>(service);
        }
    }
}