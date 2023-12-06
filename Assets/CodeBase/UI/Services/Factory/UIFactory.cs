using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.Factory;
using CodeBase.Services.GameObserver;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.UI.Windows.HUD;
using CodeBase.UI.Windows.Lose;
using CodeBase.UI.Windows.StartGame;
using CodeBase.UI.Windows.Win;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly AllServices _allServices;

        public UIFactory(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void CreateUIRoot()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            _uiRoot = Object.Instantiate(dataService.WindowData.UIRoot).transform;
        }

        public void CreateLoseWindow()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameStateMachine gameStateMachine = GetService<IGameStateMachine>();
            LoseWindow prefab = dataService.WindowData.LoseWindowPrefab;

            LoseWindow loseWindow = Object.Instantiate(prefab, _uiRoot);
            loseWindow.Construct(gameStateMachine);
        }

        public void CreateWinWindow()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameStateMachine gameStateMachine = GetService<IGameStateMachine>();
            WinWindow prefab = dataService.WindowData.WinWindowPrefab;

            WinWindow winWindow = Object.Instantiate(prefab, _uiRoot);
            winWindow.Construct(gameStateMachine);
        }

        public void CreateStartGameWindow()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameObserverService gameObserverService = GetService<IGameObserverService>();
            StartGameWindow prefab = dataService.WindowData.StartGameWindowPrefab;

            StartGameWindow startGameWindow = Object.Instantiate(prefab, _uiRoot);
            startGameWindow.Construct(gameObserverService);
        }

        public void CreateHUD()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameFactory factory = GetService<IGameFactory>();
            IInputService inputService = GetService<IInputService>();

            GameObject prefab = dataService.WindowData.HUDPrefab;

            GameObject instantiate = Object.Instantiate(prefab, _uiRoot);
            instantiate.GetComponent<CarHealthWindow>().Construct(factory, dataService);
            instantiate.GetComponentInChildren<MouseAxisButton>().Construct(inputService);
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}