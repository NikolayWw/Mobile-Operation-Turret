using CodeBase.Services.GameObserver;
using CodeBase.Services.LogicFactory;

namespace CodeBase.Services.Cleanup
{
    public class CleanupService : ICleanupService
    {
        private readonly ILogicFactory _logicFactory;
        private readonly IGameObserverService _observerService;

        public CleanupService(ILogicFactory logicFactory,IGameObserverService observerService)
        {
            _logicFactory = logicFactory;
            _observerService = observerService;
        }

        public void Cleanup()
        {
            _logicFactory.Cleanup();
            _observerService.Cleanup();
        }
    }
}