using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public Transform _parentTransform;
    public Stack<Card> Cards = new Stack<Card>();

    private const int CARD_TYPE_COUNT = 2;

    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private List<CardsFeatures> cardsFeatures = new List<CardsFeatures>();

    [SerializeField] private List<CardsFeatures> usedCardsFeatures = new List<CardsFeatures>();

    private List<Card> _cardList = new List<Card>();

    private void Awake()
    {
        if (Instance == null)
             Instance = this;
        else
            Destroy(gameObject);
    }
     
    public void SpawnCard(int spawnCount)
    {
        for (int j = 0; j < spawnCount; j++)
        {
            CardsFeatures cardFeatures = GetRandomFeature();

            for (int i = 0; i < CARD_TYPE_COUNT; i++)
            {
                GameObject cardObject = Instantiate(_cardPrefab, _parentTransform);

                if (cardObject.TryGetComponent(out Card card))
                {
                    card.SetCard(cardFeatures.CardImage, cardFeatures.CardType);
                    card.name = cardFeatures.CardType.ToString();
                    _cardList.Add(card);
                }
            }

            _cardList = ShuffleList(_cardList);

            foreach (var item in _cardList)
                Cards.Push(item);
        }
    }

    public List<Card> GetCards()
    {
        return _cardList;
    }

    private CardsFeatures GetRandomFeature()
    {
        int rndIndex = Random.Range(0, cardsFeatures.Count);
        CardsFeatures cardFeature = cardsFeatures[rndIndex];

        while (usedCardsFeatures.Contains(cardFeature))
        {
            rndIndex = Random.Range(0, cardsFeatures.Count);
            cardFeature = cardsFeatures[rndIndex];
        }

        usedCardsFeatures.Add(cardFeature);
        return cardFeature;
    }

    private List<T> ShuffleList<T>(List<T> list)
    {
        System.Random random = new System.Random();
        return list.OrderBy(item => random.Next()).ToList();
    }
}