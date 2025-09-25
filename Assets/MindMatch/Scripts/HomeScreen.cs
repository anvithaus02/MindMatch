using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();

    }
    private void OnStartGameButtonClicked()
    {
        ScreenManager.Instance.ShowScreen(Screen.LevelSelectionScreen);
    }
}
