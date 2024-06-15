using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SyncVar] [SerializeField] private bool _isMyTurn;
    public bool IsMyTurn { get { return _isMyTurn; } 
        set 
        { 
            _isMyTurn = value;
        } 
    }

    private bool isClicked = false; 

    [SerializeField] private Card _firstClickedCard;
    [SerializeField] private Card _secondClickedCard;

    private RaycastHit2D hit;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (_isMyTurn && GameManager.Instance.IsPlay)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Vector2 rayPosition;

                if (Input.touchCount > 0)
                    rayPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                else
                    rayPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                hit = Physics2D.Raycast(rayPosition, Vector2.zero);

                if (hit.collider != null && !isClicked && hit.collider.gameObject.TryGetComponent(out Card card))
                {
                    if (card.IsMatch)
                        return;

                    if (_firstClickedCard == null)
                    {
                        _firstClickedCard = card;
                        _firstClickedCard.CMDTurnUp();
                    }
                    else
                    {
                        _secondClickedCard = card;
                        _secondClickedCard.CMDTurnUp();
                    }

                    if (_firstClickedCard == _secondClickedCard)
                    {
                        _secondClickedCard = null;
                        return;
                    }

                    if (_firstClickedCard != null && _secondClickedCard != null)
                    {
                        StartCoroutine(GameManager.Instance.CheckMatch(_firstClickedCard, _secondClickedCard));
                        _firstClickedCard = null;
                        _secondClickedCard = null;
                    }
                }
            }
        }
    } 
}