using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private string _id;
    private bool _isShown;

    public Card(string suit, int rank)
    {
        _id = suit + rank.ToString();
        _isShown = false;
    }

    public string GetId()
    {
        return _id;
    }

    public string GetSpriteName()
    {
        return Utility.GetSpriteNameFromCardId(_id);
    }

    public void Flip()
    {
        _isShown = !_isShown;
    }

    public void MoveTo(Vector2 newPosition)
    {
        // transform.position = newPosition;
    }
}
