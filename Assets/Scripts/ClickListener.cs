using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
    private Card _card;

    private string layerToCheck = "Card";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
                OnClick();
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

    private void OnClick()
    {
        CardContainer deck = Utility.GetDeckContainer();
        CardContainer shownDeck = Utility.GetShownDeckContainer();
        if (deck.Contains(_card.GetId())) {
            _card.Flip();
            deck.TransferCardTo(_card, shownDeck);
        }
    }
}
