using System.Collections.Generic;
using UnityEngine;

public enum Screen
{
    HomeScreen,
    GamePlayScreen,
    LevelSelectionScreen
}

public class ScreenManager : MonoBehaviour
{
    [System.Serializable]
    public struct ScreenMapping
    {
        public Screen screenType;
        public GameObject screenObject;
    }

    [SerializeField] private List<ScreenMapping> screenMappings;

    private Dictionary<Screen, GameObject> _screenDict;
    private Stack<Screen> _screenHistory = new Stack<Screen>();
    private Screen? _currentScreen;
    public static ScreenManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _screenDict = new Dictionary<Screen, GameObject>();
        foreach (var mapping in screenMappings)
        {
            if (!_screenDict.ContainsKey(mapping.screenType) && mapping.screenObject != null)
            {
                _screenDict.Add(mapping.screenType, mapping.screenObject);
                mapping.screenObject.SetActive(false);
            }
        }

        ShowScreen(Screen.HomeScreen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void ShowScreen(Screen screenType)
    {
        if (_currentScreen.HasValue)
        {
            _screenHistory.Push(_currentScreen.Value);
            HideScreen(_currentScreen.Value);
        }

        if (_screenDict.TryGetValue(screenType, out GameObject screenObj))
        {
            screenObj.SetActive(true);
            _currentScreen = screenType;
        }
        else
        {
            Debug.LogWarning($"Screen {screenType} not found in ScreenManager!");
        }
    }

    public void HideScreen(Screen screenType)
    {
        if (_screenDict.TryGetValue(screenType, out GameObject screenObj))
        {
            screenObj.SetActive(false);
        }
    }

    public void Back()
    {
        if (_screenHistory.Count > 0)
        {
            if (_currentScreen.HasValue)
            {
                HideScreen(_currentScreen.Value);
            }

            Screen previous = _screenHistory.Pop();
            if (_screenDict.TryGetValue(previous, out GameObject screenObj))
            {
                screenObj.SetActive(true);
                _currentScreen = previous;
            }
        }
        else
        {
            Debug.Log("No previous screen in history");
        }
    }

    public void HideAllScreens()
    {
        foreach (var kvp in _screenDict)
        {
            kvp.Value.SetActive(false);
        }
        _currentScreen = null;
        _screenHistory.Clear();
    }
}
