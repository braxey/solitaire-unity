using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardContainer : CardContainer
{
    private bool needToUpdateCardPlacements = true;
    private const float RANGE = 5f;
    private const float INCREMENT = 0.5f;

    void Update()
    {
        if (needToUpdateCardPlacements && cards.Count > 0) {
            this.UpdateCardPlacements();
        }
    }

    public void UpdateCardPlacements()
    {
        float inc = getIncrement();
        for (int i = 0; i < cards.Count; i++) {
            cards[i].SetPosition(new Vector2(transform.position.x, transform.position.y - i * inc ));
            if (i == cards.Count - 1 && !cards[i].IsShown()) {
                cards[i].Flip();
            }
        }
        needToUpdateCardPlacements = false;
    }

    private float getIncrement()
    {
        if (cards.Count * INCREMENT > RANGE) {
            return RANGE / ((float) cards.Count);
        }

        return INCREMENT;
    }

    public bool WillTakeCard(Card card)
    {
        if (cards.Count > 0) {
            Card topCard = cards[cards.Count - 1];
            return (topCard.GetRank() == card.GetRank() + 1) && card.IsOppositeColor(topCard);
        } else {
            return card.GetRank() == 13;
        }
    }
}
