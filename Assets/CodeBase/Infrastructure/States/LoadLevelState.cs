using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
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
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadingCurtain;
        private readonly AllServices _allServices;

        public LoadLevelState(SceneLoader sceneLoader, LoadCurtain loadingCurtain, AllServices allServices)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _allServices = allServices;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            GetService<ICleanupService>().Cleanup();

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
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameFactory gameFactory = GetService<IGameFactory>();
            IUIFactory uiFactory = GetService<IUIFactory>();
            ILogicFactory logicFactory = GetService<ILogicFactory>();
            IGameStateMachine gameStateMachine = GetService<IGameStateMachine>();

            LevelStaticData levelData = dataService.LevelData;
            UnityEngine.Camera mainCamera = UnityEngine.Camera.main;

            gameFactory.InitGameScene(mainCamera);
            uiFactory.CreateUIRoot();
            logicFactory.InitializeEnemySpawner();

            logicFactory.EnemySpawner.Spawn();
            gameFactory.CreatePlayer(levelData.PlayerInitialPoint);
            gameFactory.CreateFinish(levelData.FinishPointPosition);

            uiFactory.CreateHUD();
            uiFactory.CreateStartGameWindow();

            gameStateMachine.Enter<LoopState>();
        }

        private static string CurrentSceneKey() =>
            SceneManager.GetActiveScene().name;

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}