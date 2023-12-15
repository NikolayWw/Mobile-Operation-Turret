using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
            StartGame();
        }

        private void StartGame()
        {
            LoadCurtain loadCurtain = new(this);
            loadCurtain.Show();

            SceneLoader sceneLoader = new(this);
            sceneLoader.Load(GameConstants.InitSceneKey, () =>
            {
                GameStateMachine stateMachine = new(sceneLoader, loadCurtain, AllServices.Container);
                stateMachine.Enter<RegisterServiceState>();
            });
        }
    }
}