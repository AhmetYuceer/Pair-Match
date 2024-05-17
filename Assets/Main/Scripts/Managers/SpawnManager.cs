using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private const int CARD_TYPE_COUNT = 2;

    [SerializeField] private List<CardsFeatures> cardsFeatures = new List<CardsFeatures>();

    [SerializeField] private List<CardsFeatures> usedCardsFeatures = new List<CardsFeatures>();

    private void Awake()
    {
        if (Instance == null)
             Instance = this;
        else
            Destroy(gameObject);
    }
     
    private void SpawnCard(int spawnCount)
    {
        for (int j = 0; j < spawnCount; j++)
        {
            CardsFeatures cardsFeatures = GetRandomFeature();

            for (int i = 0; i < CARD_TYPE_COUNT; i++)
            {

            }
        }  
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
}