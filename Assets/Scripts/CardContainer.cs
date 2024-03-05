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
        // Debug.Log(_holderBackground.transform.position);
    }

    public List<Card> GetCards()
    {
        return cards;
    }

    public void SetCards(List<Card> _cards)
    {
        this.cards = _cards;
    }

    public bool Contains(string id)
    {
        foreach (Card card in cards) {
            if (card.GetId() == id) {
                return true;
            }
        }
        return false;
    }

    public Card GetCardById(string id)
    {
        foreach (Card card in cards) {
            if (card.GetId() == id) {
                return card;
            }
        }
        return null;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
        card.SetCurrentCardContainer(this);
        // do any visual rework needed
        this.UpdateSortOrder();
    }

    public void RemoveCard(Card card)
    {
        int index = 0;
        for (int i = 0; i < cards.Count; i++) {
            if (cards[i].GetId() == card.GetId()) {
                index = i;
                break;
            }
        }
        cards.RemoveAt(index);
    }

    public void TransferCardTo(Card card, CardContainer container)
    {
        container.AddCard(card);
        this.RemoveCard(card);
    }

    private void UpdateSortOrder()
    {
        for (int i = 0; i < cards.Count; i++) {
            cards[i].SetSortOrder(i);
        }
    }
}
