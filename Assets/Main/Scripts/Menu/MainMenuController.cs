using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private UIDocument _uiDocumentMainMenu;

    private VisualElement _menuElement;
    private VisualElement _settingsElement;
    private VisualElement _levelsElement;
    private VisualElement _clientElement;

    private Button _hostButton, _clientButton, _settingsButton, _exitButton, _settingsbackButton, _levelBackButton;
    private Slider _musicSlider, _sfxSlider;

    private Button _lvlButton1, _lvlButton2, _lvlButton3, _lvlButton4, _lvlButton5, _lvlButton6, _lvlButton7, _lvlButton8, _lvlButton9, _lvlButton10, _lvlButton11, _lvlButton12;

    #region level

    private void Start()
    {
        _uiDocumentMainMenu = GetComponent<UIDocument>();
        _menuElement = _uiDocumentMainMenu.rootVisualElement.Q<VisualElement>("MainMenu");
        _settingsElement = _uiDocumentMainMenu.rootVisualElement.Q<VisualElement>("SettingsMenu");
        _levelsElement = _uiDocumentMainMenu.rootVisualElement.Q<VisualElement>("LevelMenu");
        _clientElement = _uiDocumentMainMenu.rootVisualElement.Q<VisualElement>("ClientMenu");

        #region Main Menu
        _hostButton = _menuElement.Q<Button>("HostButton");
        _clientButton = _menuElement.Q<Button>("ClientButton");
        _settingsButton = _menuElement.Q<Button>("SettingsButton");
        _exitButton = _menuElement.Q<Button>("ExitButton");

        _hostButton.clicked += OnHostButton;
        _clientButton.clicked += OnClientButton;
        _settingsButton.clicked += OnSettingsButton;
        _exitButton.clicked += OnExitButton;
        #endregion

        #region Settings Menu
        _musicSlider = _settingsElement.Q<Slider>("MusicSlider");
        _sfxSlider = _settingsElement.Q<Slider>("SfxSlider");
        _settingsbackButton = _settingsElement.Q<Button>("BackButton");

        _musicSlider.lowValue = 0f;
        _musicSlider.highValue = 1f;
        _musicSlider.value = SoundManager.Instance.GetMusicAudioSourceSoundVolume();

        _sfxSlider.lowValue = 0f;
        _sfxSlider.highValue = 1f;
        _sfxSlider.value = SoundManager.Instance.GetSfxAudioSourceSoundVolume();

        _musicSlider.RegisterValueChangedCallback(OnMusicSliderValueChanged);
        _sfxSlider.RegisterValueChangedCallback(OnSfxSliderValueChanged);
        _settingsbackButton.clicked += OnSettingsBackButton;
        #endregion

        #region Level Buttons
        _levelBackButton = _levelsElement.Q<Button>("BackButton");

        _lvlButton1 = _levelsElement.Q<Button>("lvl1");
        _lvlButton2 = _levelsElement.Q<Button>("lvl2");
        _lvlButton3 = _levelsElement.Q<Button>("lvl3");
        _lvlButton4 = _levelsElement.Q<Button>("lvl4");
        _lvlButton5 = _levelsElement.Q<Button>("lvl5");
        _lvlButton6 = _levelsElement.Q<Button>("lvl6");
        _lvlButton7 = _levelsElement.Q<Button>("lvl7");
        _lvlButton8 = _levelsElement.Q<Button>("lvl8");
        _lvlButton9 = _levelsElement.Q<Button>("lvl9");
        _lvlButton10 = _levelsElement.Q<Button>("lvl10");
        _lvlButton11 = _levelsElement.Q<Button>("lvl11");
        _lvlButton12 = _levelsElement.Q<Button>("lvl12");

        _lvlButton1.clicked += OneLevel;
        _lvlButton2.clicked += TwoLevel;
        _lvlButton3.clicked += ThreeLevel;
        _lvlButton4.clicked += FourLevel;
        _lvlButton5.clicked += FiveLevel;
        _lvlButton6.clicked += SixLevel;
        _lvlButton7.clicked += SevenLevel;
        _lvlButton8.clicked += EightLevel;
        _lvlButton9.clicked += NineLevel;
        _lvlButton10.clicked += TenLevel;
        _lvlButton11.clicked += ElevenLevel;
        _lvlButton12.clicked += TwelveLevel;

        _levelBackButton.clicked += OnLevelBackButton;
        #endregion

        LevelLockControl();
    }

    private void OnClientButton()
    {
        CustomNetworkManager.Instance.JoinHost();
    }

    private void LevelLockControl()
    {
        List<SOGameSettings> levels = LevelManager.Instance.GetAllLevels();

        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].Locked)
            {
                _levelsElement.Q<VisualElement>($"lvl{i + 1}Lock").visible = true;
            }
            else
            {
                _levelsElement.Q<VisualElement>($"lvl{i + 1}Lock").visible = false;
            }
        }
    }

    private void OneLevel()
    {
        LevelManager.Instance.StartLevel(0); 
        CustomNetworkManager.Instance.CreateHost();
    }
    private void TwoLevel()
    {
        LevelManager.Instance.StartLevel(1);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void ThreeLevel()
    {
        LevelManager.Instance.StartLevel(2);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void FourLevel()
    {
        LevelManager.Instance.StartLevel(3);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void FiveLevel()
    {
        LevelManager.Instance.StartLevel(4);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void SixLevel()
    {
        LevelManager.Instance.StartLevel(5);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void SevenLevel()
    {
        LevelManager.Instance.StartLevel(6);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void EightLevel()
    {
        LevelManager.Instance.StartLevel(7);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void NineLevel()
    {
        LevelManager.Instance.StartLevel(8);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void TenLevel()
    {
        LevelManager.Instance.StartLevel(9);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void ElevenLevel()
    {
        LevelManager.Instance.StartLevel(10);
        CustomNetworkManager.Instance.CreateHost();
    }
    private void TwelveLevel()
    {
        LevelManager.Instance.StartLevel(11);
        CustomNetworkManager.Instance.CreateHost();
    }
    #endregion

    private void OnLevelBackButton()
    {
        _settingsElement.style.display = DisplayStyle.None;
        _levelsElement.style.display = DisplayStyle.None;
        _menuElement.style.display = DisplayStyle.Flex;
    }

    private void OnMusicSliderValueChanged(ChangeEvent<float> evt)
    {
        float value = evt.newValue; 
        SoundManager.Instance.SetMusicAudioSourceSoundVolume(value);
    }
    private void OnSfxSliderValueChanged(ChangeEvent<float> evt)
    {
        float value = evt.newValue;
        SoundManager.Instance.SetSfxAudioSourceSoundVolume(value);
    }

    private void OnHostButton()
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