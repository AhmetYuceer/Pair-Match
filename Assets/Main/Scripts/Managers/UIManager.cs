using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UIDocument _uýDocument;

    private VisualElement _timeElement;
    private VisualElement _endElement;

    private Button _nextLevelButton, _mainMenuButton, _endElementMainMenuButton;
    private Label _endElementTimer, _timer;

    private int hours, minutes, seconds;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _uýDocument = GetComponent<UIDocument>();

        _timeElement = _uýDocument.rootVisualElement.Q<VisualElement>("TimeElement");
        _endElement = _uýDocument.rootVisualElement.Q<VisualElement>("EndElement");

        _nextLevelButton = _endElement.Q<Button>("NextLevel");
        _endElementMainMenuButton = _endElement.Q<Button>("MainMenu");
        _mainMenuButton = _timeElement.Q<Button>("MainMenu");
        _endElementTimer = _endElement.Q<Label>("EndElementTimer");

        _timer = _timeElement.Q<Label>("Time");

        _nextLevelButton.clicked += NextLevel;
        _endElementMainMenuButton.clicked += MainMenu;
        _mainMenuButton.clicked += MainMenu;
    }

    private void Update()
    {
        hours = GameManager.Instance.hours;
        minutes = GameManager.Instance.minutes;
        seconds = GameManager.Instance.seconds;

        _timer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        _endElementTimer.text = _timer.text;
    }

    public void VisibleEndElement(bool value)
    {
        _endElement.visible = value;
    }

    private void NextLevel()
    {
        LevelManager.Instance.LevelComplated(GameManager.Instance._soGameSettings);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}