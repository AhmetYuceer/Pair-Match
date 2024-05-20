using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SOGameSettings _soGameSettings;
    
    public bool IsClickable;

    public int hours, minutes, seconds;
    private float _timer;
    private bool _timerEnable;

    private List<Card> cards;
    private Stack<Card> clickedCard = new Stack<Card>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UIManager.Instance.VisibleEndElement(false);

       _soGameSettings  = LevelManager.Instance.SOGameSettings;

        GridManager.Instance.CreateGrid(_soGameSettings.GridX, _soGameSettings.GridY);
        SpawnManager.Instance.SpawnCard(_soGameSettings.CardCount);

        GridManager.Instance.FillGrid(SpawnManager.Instance.Cards);
        SetScreen();

        IsClickable = true;
        _timerEnable = true;
    }

    private void Update()
    {
        if (_timerEnable)
        {
            _timer += Time.deltaTime;

            _timer += Time.deltaTime;

            hours = (int)(_timer / 3600);
            minutes = (int)((_timer % 3600) / 60);
            seconds = (int)(_timer % 60);
        }
    }

    public void AddClickedCard(Card card)
    {
        if (!clickedCard.Contains(card))
        {
            card.TurnUp();
            clickedCard.Push(card);

            if (clickedCard.Count >= 2)
            {
                IsClickable = false;
                StartCoroutine(CheckMatch());
            }
        }
    }

    private IEnumerator CheckMatch()
    {
        Card card = clickedCard.Pop();
        Card card1 = clickedCard.Pop();

        if (card.CardType == card1.CardType)
        {
            yield return new WaitForSeconds(0.3f);

            card.IsMatch = true;
            card1.IsMatch = true;
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            card.TurnDown();
            card1.TurnDown();
        }
        IsClickable = true;

        int count = 0;

        foreach (var item in cards)
        {
            if (item.IsMatch)
            {
                count++;
            }
        }

        if (count >= cards.Count)
        {
            _timerEnable = false;
            _soGameSettings.IsComplated = true;

            SoundManager.Instance.LevelComplateSfx();
            LevelManager.Instance.CheckLevel(_soGameSettings);
            UIManager.Instance.VisibleEndElement(true);
        }
    }

    private void SetScreen()
    {
        cards = SpawnManager.Instance.GetCards();
        Vector3 centerPoint = CalculateCenterPoint();
        CenterCamera(centerPoint);
    }

    private Vector3 CalculateCenterPoint()
    {
        Vector3 totalPosition = Vector3.zero;

        foreach (var obj in cards)
        {
            totalPosition += obj.transform.position;
        }

        return totalPosition / cards.Count;
    }

    private void CenterCamera(Vector3 centerPoint)
    {
        Camera.main.orthographicSize = _soGameSettings.CameraSize;
        Camera.main.transform.position = new Vector3(centerPoint.x, centerPoint.y, Camera.main.transform.position.z);
    }
}