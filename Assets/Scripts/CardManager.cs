using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private List<Card> fullDeck = new List<Card>();
    [SerializeField] public GameObject cardPreFab;

    void Awake()
    {
        // initialize all the cards
        foreach (string suit in Utility.SuitMap.Keys) {
            foreach (int rank in Utility.Ranks) {
                fullDeck.Add(new Card(suit, rank));
            }
        }
        fullDeck.Shuffle();

        // distribute cards to containers

        // board containers
        List<CardContainer> boardContainers = Utility.GetSortedBoardContainers();

        int numberOfContainers = boardContainers.Count;
        int maxCardsInContainer = 7;
        int cardsUsed = 0;
        for (int i = numberOfContainers - 1; i >= 0; i--) {
            for (int j = 0; j < maxCardsInContainer; j++) {
                Debug.Log(i + " " + j);

                boardContainers[i].AddCard(fullDeck[cardsUsed]);
                cardsUsed++;
            }
            maxCardsInContainer--;
        }

        // deck container
        CardContainer deckContainer = Utility.GetDeckContainer();

        List<Card> remainingCards = fullDeck.GetRange(cardsUsed - 1, 52 - cardsUsed);
        foreach (Card card in remainingCards) {
            deckContainer.AddCard(card);
        }

        // clear remaining cards
        remainingCards.Clear();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
