using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System.Collections; 
public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    public bool IsPlay;

    [SyncVar] public List<PlayerController> players = new List<PlayerController>();
    public List<Card> Cards = new List<Card>();

    [SyncVar] public SOGameSettings _soGameSettings;

    [SyncVar] private bool _timerEnable;
    
    [SyncVar] public int hours, minutes, seconds;
    [SyncVar] private float _timer;

    [SyncVar] public Vector3 CamPos = Vector3.zero;
    [SyncVar] public float OrthographicSize;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (isServer)
        {
            _soGameSettings = LevelManager.Instance.SOGameSettings;
            GridManager.Instance.CMDCreateGrid(_soGameSettings.GridX, _soGameSettings.GridY);
            SpawnManager.Instance.SpawnCard(_soGameSettings.CardCount);
        }
    }

    bool playersReady = false;
    bool endGame = false;
     
    private void Update()
    {
        if (players.Count > 1 && !playersReady && !endGame)
        {
            SetScreen();
            players[0].IsMyTurn = true;
            IsPlay = true;
            playersReady = true;
            UIManager.Instance.ChangeTurnText("Player 1's Turn");
        }
        else if(players.Count < 2 && !endGame)
        {
            UIManager.Instance.ChangeTurnText("wait for other player");
            IsPlay = false;
        }
  

        if (IsPlay)
        {
            _timerEnable = true;
            if (_timerEnable)
            {
                _timer += Time.deltaTime;

                hours = (int)(_timer / 3600);
                minutes = (int)((_timer % 3600) / 60);
                seconds = (int)(_timer % 60);
            }

            int count = 0;

            foreach (var item in Cards)
            {
                if (item.IsMatch)
                {
                    count++;
                }
            }

            if (count >= Cards.Count)
            {
                endGame = true;
                IsPlay = false;
                _timerEnable = false;
                _soGameSettings.IsComplated = true;
                SoundManager.Instance.LevelComplateSfx();
                LevelManager.Instance.CheckLevel(_soGameSettings);
                UIManager.Instance.VisibleEndElement(true);
            }
        }
    }

    public IEnumerator CheckMatch(Card firstClickedCard, Card secondClickedCard)
    {
        if (firstClickedCard.CardType == secondClickedCard.CardType)
        {
            yield return new WaitForSeconds(0.3f);

            firstClickedCard.CMDMatch();
            secondClickedCard.CMDMatch();
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            firstClickedCard.CMDTurnDown();
            secondClickedCard.CMDTurnDown();
        }
        CMDNextTurn();
    }

    private void FindCards()
    {
        Cards.Clear();
        GameObject[] cardsObjects = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject item in cardsObjects)
        {
            Card card = item.GetComponent<Card>();
            Cards.Add(card);
        }
    }

    [Command(requiresAuthority = false)]
    public void CMDNextTurn()
    {
        RPCNextTurn();
    }

    [ClientRpc]
    private void RPCNextTurn()
    {
        if (players[0].IsMyTurn)
        {
            players[0].IsMyTurn = false;
            players[1].IsMyTurn = true;
        }
        else
        {
            players[0].IsMyTurn = true;
            players[1].IsMyTurn = false;
        }

        UIManager.Instance.ChangeTurnText(players[0].IsMyTurn ? "Player 1's Turn" : "Player 2's Turn");
    }

    [Command(requiresAuthority = false)]
    public void CMDAddPlayer(NetworkIdentity Player)
    {
        List<PlayerController> newPlayers = players;
        newPlayers.Add(Player.gameObject.GetComponent<PlayerController>());
        RPCAddPlayer(newPlayers);
    }

    [ClientRpc]
    private void RPCAddPlayer(List<PlayerController> players)
    {
        this.players = players;

        if (players.Count == 1 && players[0] == null)
        {
            players[0].IsMyTurn = true;
            Debug.Log(players[0].IsMyTurn);
        }
    }
    
    private void SetScreen()
    {
        FindCards();
        CalculateCenterPoint();
    }

    private void CalculateCenterPoint()
    {
        Vector3 totalPosition = Vector3.zero;

        foreach (var obj in Cards)
        {
            totalPosition += obj.transform.position;
        }

        totalPosition = totalPosition / Cards.Count;

        OrthographicSize = _soGameSettings.CameraSize;
        CamPos = new Vector3(totalPosition.x, totalPosition.y, Camera.main.transform.position.z);

        Camera.main.orthographicSize = OrthographicSize;
        Camera.main.transform.position = CamPos;
    }

}