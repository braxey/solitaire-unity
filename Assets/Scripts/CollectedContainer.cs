using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedContainer : CardContainer
{
    public bool WillTakeCard(Card card)
    {
        if (cards.Count > 0) {
            Card topCard = cards[cards.Count - 1];
            return (topCard.GetRank() == card.GetRank() - 1) && card.IsSameSuit(topCard);
        } else {
            return card.GetRank() == 1;
        }
    }
}
