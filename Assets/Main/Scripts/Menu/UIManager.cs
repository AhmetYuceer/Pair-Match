using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UIDocument _uiDocument;

    private VisualElement _timeElement;
    private VisualElement _endElement; 

    private Button _mainMenuButton, _endElementMainMenuButton;
    private Label _endElementTimer, _timer, _turnLabel;

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
        VisibleEndElement(false);
    }

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();

        _timeElement = _uiDocument.rootVisualElement.Q<VisualElement>("TimeElement");
        _endElement = _uiDocument.rootVisualElement.Q<VisualElement>("EndElement");
       

        _endElementMainMenuButton = _endElement.Q<Button>("MainMenu");
        _mainMenuButton = _timeElement.Q<Button>("MainMenu");
        _endElementTimer = _endElement.Q<Label>("EndElementTimer");

        _timer = _timeElement.Q<Label>("Time"); 
        _turnLabel = _timeElement.Q<Label>("Turn");

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

    public void ChangeTurnText(string text)
    {
        _turnLabel.text = text;
    } 

    public void VisibleEndElement(bool value)
    {
        _endElement.visible = value;
    }

    private void MainMenu()
    {
        CustomNetworkManager.Instance.LoadMainMenu();
    }
}