using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void AddCard(Card card)
    {
        cards.Add(card);

        // do any visual rework needed
    }
}
