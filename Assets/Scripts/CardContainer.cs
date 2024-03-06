using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    protected List<Card> cards = new List<Card>();
    [SerializeField] protected GameObject _holderBackground;

    void Awake()
    {
        this.Reset();
    }

    public void Reset()
    {
        cards.Clear();
        transform.position = _holderBackground.transform.position;
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
        this.UpdateSortOrder();

        if (cards.Count > 0 && this.IsABoardContainer()) {
            if (!cards[cards.Count - 1].IsShown()) {
                cards[cards.Count - 1].Flip();
            }
        }
    }

    public void TransferCardTo(Card card, CardContainer container)
    {
        container.AddCard(card);
        this.RemoveCard(card);
    }

    public bool isTheSameAs(CardContainer container) {
        return this.transform.gameObject.name == container.transform.gameObject.name;
    }

    public bool IsABoardContainer()
    {
        return transform.gameObject.name.Contains("BoardHolder");
    }

    public bool IsACollectedContainer()
    {
        return transform.gameObject.name.Contains("CollectedHolder");
    }

    private void UpdateSortOrder()
    {
        for (int i = 0; i < cards.Count; i++) {
            cards[i].SetSortOrder(i);
        }
    }

    public bool TopCardIs(Card card)
    {
        return cards.Count > 0 && cards[cards.Count - 1].GetId() == card.GetId();
    }

    public List<Card> GetCardsToTransfer(Card card)
    {
        int index = 0;
        for (int i = 0; i < cards.Count; i++) {
            if (cards[i].GetId() == card.GetId()) {
                index = i;
                break;
            }
        }

        return cards.GetRange(index, cards.Count - index);
    }
}
