using UnityEngine;

public class Card : IClickable
{
    public SpriteRenderer _cardSpriteRenderer;

    public GameObject CardImageObject;
    public CardsType CardType;

    public Sprite UpCardSprite;
    public Sprite DownCardSprite;

    public bool IsMatch;

    private void Start()
    {
        _cardSpriteRenderer = GetComponent<SpriteRenderer>();
        TurnDown();
    }

    public void SetCard(Sprite cardSprite, CardsType cardType)
    {
        if (CardImageObject.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            CardType = cardType;
            spriteRenderer.sprite = cardSprite;
        }
    }

    public void TurnUp()
    {
        SoundManager.Instance.PlayCardOpenSfx();
        _cardSpriteRenderer.sprite = UpCardSprite;
        CardImageObject.SetActive(true);
    }
    
    public void TurnDown()
    {
        _cardSpriteRenderer.sprite = DownCardSprite;
        CardImageObject.SetActive(false);
    }
}