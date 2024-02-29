using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    private List<Card> cards = new List<Card>();
    [SerializeField] private GameObject _holderBackground;

    void Awake()
    {
        transform.position = _holderBackground.transform.position;
    }

    public List<Card> GetCards()
    {
        return cards;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
        card.SetCurrentCardContainer(this);
        // do any visual rework needed
    }

    public void RemoveCard(Card card)
    {
        int lastIndex = cards.Count - 1;
        if (cards[lastIndex].GetId() == card.GetId()) {
            cards.RemoveAt(lastIndex);
        }
    }

    public void TransferCardTo(Card card, CardContainer container)
    {
        container.AddCard(card);
        this.RemoveCard(card);
    }
}
