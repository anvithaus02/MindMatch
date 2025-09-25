using System.Collections;
using System.Collections.Generic;
using MindMatch.UI;
using UnityEngine;
using UnityEngine.UI;
namespace MindMatch.Gameplay.UI
{
    public class HomeScreen : MonoBehaviour
    {
        [SerializeField] private ActionButton _startGameButton;

        private void OnEnable()
        {
            AudioManager.Instance.PlayAudio(AudioType.BackgroundMusic);
            _startGameButton.Initialize("START", OnStartGameButtonClicked);
        }
        private void OnStartGameButtonClicked()
        {
            ScreenManager.Instance.ShowScreen(Screen.LevelSelectionScreen);
        }
    }
}