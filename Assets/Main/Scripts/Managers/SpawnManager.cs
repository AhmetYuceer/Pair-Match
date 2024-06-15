using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Mirror;

public class SpawnManager : NetworkBehaviour
{
    public static SpawnManager Instance;

    public Transform _parentTransform;

    private const int CARD_TYPE_COUNT = 2;

    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private List<CardsFeatures> cardsFeatures = new List<CardsFeatures>();

    [SerializeField] private List<CardsFeatures> usedCardsFeatures = new List<CardsFeatures>();

    public SyncList<Card> CardList = new SyncList<Card>();

 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Server]
    public void SpawnCard(int spawnCount)
    {
        for (int j = 0; j < spawnCount; j++)
        {
            CardsFeatures cardFeatures = GetRandomFeature();

            for (int i = 0; i < CARD_TYPE_COUNT; i++)
            {
                GameObject cardObject = Instantiate(_cardPrefab, _parentTransform);
                NetworkServer.Spawn(cardObject.gameObject);

                Card card = cardObject.GetComponent<Card>();
                CardList.Add(card);
                card.SetCardSprites(cardFeatures.CardSprite.name, cardFeatures.CardType);
                card.name = cardFeatures.CardType.ToString();
            }
        }
        CardList = ShuffleList(CardList);
        GridManager.Instance.FillGrid(CardList.ToList());
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

    private SyncList<T> ShuffleList<T>(SyncList<T> list)
    {
        System.Random random = new System.Random();
        List<T> shuffledList = list.OrderBy(item => random.Next()).ToList();
        return new SyncList<T>(shuffledList);
    }
}