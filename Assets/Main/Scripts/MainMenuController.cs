using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private UIDocument _uýDocument;

    private Button _startButton, _settingsButton, _exitButton;

    private void Awake()
    {
        _uýDocument = GetComponent<UIDocument>();

        _startButton = _uýDocument.rootVisualElement.Q<Button>("StartButton");
        _settingsButton = _uýDocument.rootVisualElement.Q<Button>("SettingsButton");
        _exitButton = _uýDocument.rootVisualElement.Q<Button>("ExitButton");

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