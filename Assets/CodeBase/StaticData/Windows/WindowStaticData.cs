using CodeBase.UI.Windows.Lose;
using CodeBase.UI.Windows.StartGame;
using CodeBase.UI.Windows.Win;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(menuName = "Static Data/Window static data")]
    public class WindowStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject UIRoot { get; private set; }
        [field: SerializeField] public GameObject HUDPrefab { get; private set; }
        [field: SerializeField] public LoseWindow LoseWindowPrefab { get; private set; }
        [field: SerializeField] public WinWindow WinWindowPrefab { get; private set; }
        [field: SerializeField] public StartGameWindow StartGameWindowPrefab { get; private set; }
    }
}