using Mirror;
using UnityEngine;

public class Card : NetworkBehaviour
{
    public GameObject CardImageObject;
    private SpriteRenderer _cardSpriteRenderer, _cardImageSpriteRenderer;

    private const string UpCardSpriteName = "Card_0";
    private const string DownCardSpriteName = "Card_BG_1";

    [SyncVar(hook = nameof(OnCardImageSpriteNameChanged))]
    public string CardImageSpriteName;

    [SyncVar]
    public CardsType CardType;

    [SyncVar]
    public bool IsMatch;

    private void Start()
    {
        _cardSpriteRenderer = GetComponent<SpriteRenderer>();
        _cardImageSpriteRenderer = CardImageObject.GetComponent<SpriteRenderer>();

        UpdateCardSprites();
    }

    [Command(requiresAuthority = false)]
    public void CMDMatch()
    {
        RPCMatch();
    }

    [ClientRpc]
    public void RPCMatch()
    {
        IsMatch = true;
    }

    [Server]
    public void SetCardSprites(string spriteName, CardsType cardType)
    {
        CardImageSpriteName = spriteName;
        CardType = cardType;
    }

    private void OnCardImageSpriteNameChanged(string oldSpriteName, string newSpriteName)
    {
        UpdateCardSprites();
    }

    private void UpdateCardSprites()
    {
        if (_cardSpriteRenderer == null)
        {
            _cardSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (_cardImageSpriteRenderer == null)
        {
            _cardImageSpriteRenderer = CardImageObject.GetComponent<SpriteRenderer>();
        }

        _cardSpriteRenderer.sprite = Load("Card", UpCardSpriteName);
        _cardImageSpriteRenderer.sprite = Load("animals-ai", CardImageSpriteName);

        CMDTurnDown();
    }

    Sprite Load(string imageName, string spriteName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(imageName);

        foreach (var s in sprites)
        {
            if (s.name == spriteName)
            {
                return s;
            }
        }
         
        imageName = "fruits-ai";
        sprites = Resources.LoadAll<Sprite>(imageName);

        foreach (var s in sprites)
        {
            if (s.name == spriteName)
            {
                return s;
            }
        }

        return null;
    }

    [Command(requiresAuthority = false)]
    public void CMDTurnUp()
    {
        RPCTurnUp();
    }

    [ClientRpc]
    public void RPCTurnUp()
    {
        SoundManager.Instance.PlayCardOpenSfx();
        _cardSpriteRenderer.sprite = Load("Card", UpCardSpriteName);
        CardImageObject.SetActive(true);
    }

    [Command(requiresAuthority = false)]
    public void CMDTurnDown()
    {
        RPCTurnDown();
    }

    [ClientRpc]
    public void RPCTurnDown()
    {
        CardImageObject.SetActive(false);
        _cardSpriteRenderer.sprite = Load("Card_BG", DownCardSpriteName);
    }
}
