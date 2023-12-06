using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Level;
using CodeBase.UI.Services.Factory;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ICleanupService _cleanupService;
        private readonly ILogicFactory _logicFactory;
        private readonly IStaticDataService _dataService;

        public LoadLevelState(IGameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadCurtain loadingCurtain,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            ICleanupService cleanupService,
            ILogicFactory logicFactory,
            IStaticDataService dataService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _cleanupService = cleanupService;
            _logicFactory = logicFactory;
            _dataService = dataService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _cleanupService.Cleanup();

            if (CurrentSceneKey() != GameConstants.ReloadSceneKey)
                _sceneLoader.Load(GameConstants.ReloadSceneKey, () => _sceneLoader.Load(sceneName, OnLoaded));
            else
                _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            LevelStaticData levelData = _dataService.LevelData;
            UnityEngine.Camera mainCamera = UnityEngine.Camera.main;

            _gameFactory.InitGameScene(mainCamera);
            _uiFactory.CreateUIRoot();
            _logicFactory.InitializeEnemySpawner();

            _logicFactory.EnemySpawner.Spawn();
            _gameFactory.CreatePlayer(levelData.PlayerInitialPoint);//start position
            _gameFactory.CreateFinish(levelData.FinishPointPosition);

            _uiFactory.CreateHUD();
            _uiFactory.CreateStartGameWindow();

            _stateMachine.Enter<LoopState>();
        }

        private static string CurrentSceneKey() =>
            SceneManager.GetActiveScene().name;
    }
}