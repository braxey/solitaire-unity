using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
    private Card _card;
    private string layerToCheck = "Card";
    private bool justFlipped = false;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            justFlipped = false;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            GameObject topCard = null;
            bool hitDeck = false;
            foreach (RaycastHit2D hit in hits) {
                SpriteRenderer sr = hit.transform.GetComponent<SpriteRenderer>();
            
                if (sr != null && sr.sortingLayerName == layerToCheck) {
                    if (topCard == null) {
                        topCard = sr.transform.gameObject;
                    }

                    if (sr.sortingOrder > topCard.GetComponent<SpriteRenderer>().sortingOrder) {
                        topCard = hit.transform.gameObject;
                    }
                } else if (sr != null && sr.transform.gameObject.name == "deck") {
                    hitDeck = true;
                }
            }

            if (topCard != null) {
                _card = Utility.GetCardFromId(topCard);
                OnCardClick();
            } else if (hitDeck) {
                RecycleDeck();
            }
        }
    }

    private void RecycleDeck()
    {
        CardContainer shown = Utility.GetShownDeckContainer();
        List<Card> shownCards = shown.GetCards();

        CardContainer deck = Utility.GetDeckContainer();

        List<Card> toMoveToDeck = new List<Card>(shownCards);
        toMoveToDeck.Reverse();

        foreach (Card card in toMoveToDeck) {
            card.Flip();
            shown.RemoveCard(card);
            deck.AddCard(card);
        }
    }

    private void OnCardClick()
    {
        DeckListener();
        MoveListener();
    }

    private void DeckListener()
    {
        CardContainer deck = Utility.GetDeckContainer();
        CardContainer shownDeck = Utility.GetShownDeckContainer();
        if (deck.Contains(_card.GetId())) {
            _card.Flip();
            deck.TransferCardTo(_card, shownDeck);
            justFlipped = true;
        }
    }

    private void MoveListener()
    {
        List<CardContainer> validMoves = getValidMoves(_card);
        if (validMoves.Count > 0) {
            MoveCard(validMoves);
        }
    }

    private List<CardContainer> getValidMoves(Card card)
    {
        CardContainer currentContainer = card.GetCurrentCardContainer();
        List<CardContainer> validMoves = new List<CardContainer>();

        List<CardContainer> boardContainers = Utility.GetSortedBoardContainers();
        foreach (BoardContainer container in boardContainers) {
            if (currentContainer.isTheSameAs(container) || !card.IsShown() || justFlipped) {
                continue;
            }

            if (container.WillTakeCard(card)) {
                validMoves.Add(container);
            }
        }

        return validMoves;
    }

    private void MoveCard(List<CardContainer> validMoves)
    {
        foreach (CardContainer validMove in validMoves) {
            if (validMove.GetType() == typeof(BoardContainer)) {
                CardContainer currentContainer = _card.GetCurrentCardContainer();

                if (currentContainer.IsABoardContainer()) {
                    if (currentContainer.TopCardIs(_card)) {
                        SingleCardTransfer(_card, validMove);
                    } else {
                        BulkTransfer(_card, validMove);
                    }
                    ((BoardContainer)currentContainer).UpdateCardPlacements();
                    break;
                } else {
                    SingleCardTransfer(_card, validMove);
                    break;
                }
                
            }
        }
    }

    private void SingleCardTransfer(Card card, CardContainer destination)
    {
        CardContainer currentContainer = _card.GetCurrentCardContainer();

        // move card to new container and update visuals
        currentContainer.TransferCardTo(_card, destination);
        // if (!_card.IsShown()) {
        //     _card.Flip();
        // }

        ((BoardContainer) destination).UpdateCardPlacements();

        justFlipped = true;
    }

    private void BulkTransfer(Card card, CardContainer destination)
    {
        CardContainer currentContainer = _card.GetCurrentCardContainer();

        // move cards to new container and update visuals
        List<Card> cardsToTransfer = new List<Card>();
        currentContainer.GetCardsToTransfer(_card);

        foreach (Card c in cardsToTransfer) {
            currentContainer.TransferCardTo(c, destination);
            // if (!_card.IsShown()) {
            //     _card.Flip();
            // }
        }

        ((BoardContainer) destination).UpdateCardPlacements();

        justFlipped = true;
    }
}
