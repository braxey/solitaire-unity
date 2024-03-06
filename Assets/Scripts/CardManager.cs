using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private List<Card> fullDeck;
    [SerializeField] public GameObject cardPreFab;

    void Start()
    {
        fullDeck = new List<Card>();

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
                boardContainers[i].AddCard(fullDeck[cardsUsed]);
                cardsUsed++;
            }
            maxCardsInContainer--;
            ((BoardContainer) boardContainers[i]).UpdateCardPlacements();
        }

        // deck container
        CardContainer deckContainer = Utility.GetDeckContainer();

        List<Card> remainingCards = fullDeck.GetRange(cardsUsed, 52 - cardsUsed);
        foreach (Card card in remainingCards) {
            deckContainer.AddCard(card);
        }


        // clear remaining cards
        remainingCards.Clear();
    }

    public void RestartGame()
    {
        // destroy all card objects
        GameObject[] allCardObjects = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject obj in allCardObjects)
        {
            Destroy(obj);
        }

        // reset all containers
        List<CardContainer> containers = Utility.GetAllCardContainers();

        foreach (CardContainer container in containers) {
            container.Reset();
        }

        // reshuffle and deal cards
        this.Start();
    }
}
