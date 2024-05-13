using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private UIDocument _u�Document;

    private Button _startButton, _settingsButton, _exitButton;

    private void Awake()
    {
        _u�Document = GetComponent<UIDocument>();

        _startButton = _u�Document.rootVisualElement.Q<Button>("StartButton");
        _settingsButton = _u�Document.rootVisualElement.Q<Button>("SettingsButton");
        _exitButton = _u�Document.rootVisualElement.Q<Button>("ExitButton");

        _startButton.clicked += StartGame;
        _settingsButton.clicked += SettingsButton;
        _exitButton.clicked += ExitButton;
    }

    private void StartGame()
    {
        Debug.Log("StartGame");
    }

    private void SettingsButton()
    {
        Debug.Log("SettingsButton");

    }

    private void ExitButton()
    {
        Debug.Log("ExitButton");

    }

}