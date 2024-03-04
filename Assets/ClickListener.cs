using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
    private Card _card;

    void Awake()
    {
        // _card = 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world point (for 2D)
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Cast a 2D ray
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Your click handling logic goes here
                if (hit.collider.gameObject == transform.gameObject) {
                    _card = Utility.GetCardFromId(transform.gameObject);
                    OnClick();
                }
            }
        }
    }

    void OnClick()
    {
        CardContainer deck = Utility.GetDeckContainer();
        CardContainer shownDeck = Utility.GetShownDeckContainer();
        if (deck.Contains(_card.GetId())) {
            _card.Flip();
            deck.TransferCardTo(_card, shownDeck);
        }
    }
}
