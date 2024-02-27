using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private List<Card> fullDeck = new List<Card>();

    void Awake()
    {
        // initialize all the cards
        foreach (string suit in Utility.SuitMap.Keys) {
            foreach (int rank in Utility.Ranks) {
                fullDeck.Add(new Card(suit, rank));
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
