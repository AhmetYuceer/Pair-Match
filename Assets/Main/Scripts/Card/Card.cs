using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer _cardSpriteRenderer;
    private SpriteRenderer _imageRenderer;

    public GameObject CardImage;
    public CardsType CardType;

    public Sprite UpCardSprite;
    public Sprite DownCardSprite;

    private void Start()
    {
        _cardSpriteRenderer = GetComponent<SpriteRenderer>();
        _imageRenderer = CardImage.GetComponent<SpriteRenderer>();
        TurnUp();
    }

    public void SetCardImage(Sprite cardImage)
    {
        _imageRenderer.sprite = cardImage;
    }

    public void TurnUp()
    {
        _cardSpriteRenderer.sprite = UpCardSprite;
        CardImage.SetActive(true);
    }
    
    public void TurnDown()
    {
        _cardSpriteRenderer.sprite = DownCardSprite;
        CardImage.SetActive(false);
    }
}