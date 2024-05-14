using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private UIDocument _uýDocumentMainMenu;

    private VisualElement _menuElement;
    private VisualElement _settingsElement;
    private VisualElement _levelsElement;

    private Button _startButton, _settingsButton, _exitButton, _settingsbackButton, _levelBackButton;
    private Button _lvlButton1;
    private Slider _musicSlider, _sfxSlider;

    private void Awake()
    {
        _uýDocumentMainMenu = GetComponent<UIDocument>();
        _menuElement = _uýDocumentMainMenu.rootVisualElement.Q<VisualElement>("MainMenu");
        _settingsElement = _uýDocumentMainMenu.rootVisualElement.Q<VisualElement>("SettingsMenu");
        _levelsElement = _uýDocumentMainMenu.rootVisualElement.Q<VisualElement>("LevelMenu");

        //Main Menu
        _startButton = _menuElement.Q<Button>("StartButton");
        _settingsButton = _menuElement.Q<Button>("SettingsButton");
        _exitButton = _menuElement.Q<Button>("ExitButton");

        _startButton.clicked += OnStartButton;
        _settingsButton.clicked += OnSettingsButton;
        _exitButton.clicked += OnExitButton;

        //Settings Menu
        _musicSlider = _settingsElement.Q<Slider>("MusicSlider");
        _sfxSlider = _settingsElement.Q<Slider>("SfxSlider");
        _settingsbackButton = _settingsElement.Q<Button>("BackButton");

        _musicSlider.lowValue = 0f;
        _musicSlider.highValue = 1f;
        _musicSlider.value = SoundManager.Instance.GetMusicAudioSourceSoundVolume();

        _sfxSlider.lowValue = 0f;
        _sfxSlider.highValue= 1f;
        _sfxSlider.value = SoundManager.Instance.GetSfxAudioSourceSoundVolume();

        _musicSlider.RegisterValueChangedCallback(OnMusicSliderValueChanged);
        _sfxSlider.RegisterValueChangedCallback(OnSfxSliderValueChanged);
        _settingsbackButton.clicked += OnSettingsBackButton;

        //Level Menu
        _levelBackButton = _levelsElement.Q<Button>("BackButton");
        _lvlButton1 = _levelsElement.Q<Button>("lvl1");

        _lvlButton1.clicked += StartGame;
        _levelBackButton.clicked += OnLevelBackButton;
    }


    private void OnLevelBackButton()
    {
        _settingsElement.style.display = DisplayStyle.None;
        _levelsElement.style.display = DisplayStyle.None;
        _menuElement.style.display = DisplayStyle.Flex;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OnMusicSliderValueChanged(ChangeEvent<float> evt)
    {
        float value = evt.newValue;
        Debug.Log("Slider deðeri: " + value);
        SoundManager.Instance.SetMusicAudioSourceSoundVolume(value);
    }
    private void OnSfxSliderValueChanged(ChangeEvent<float> evt)
    {
        float value = evt.newValue;
        SoundManager.Instance.SetSfxAudioSourceSoundVolume(value);
    }

    private void OnStartButton()
    {
        _menuElement.style.display = DisplayStyle.None;
        _settingsElement.style.display = DisplayStyle.None;
        _levelsElement.style.display = DisplayStyle.Flex;
    }

    private void OnSettingsButton()
    {
        _menuElement.style.display = DisplayStyle.None;
        _levelsElement.style.display = DisplayStyle.None;
        _settingsElement.style.display = DisplayStyle.Flex;
    }

    private void OnExitButton()
    {
        Application.Quit();
    }

    private void OnSettingsBackButton() 
    {
        _levelsElement.style.display = DisplayStyle.None;
        _settingsElement.style.display = DisplayStyle.None;
        _menuElement.style.display = DisplayStyle.Flex;
    }
}