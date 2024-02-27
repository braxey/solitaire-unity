using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private string _id;
    private bool _isShown;
    private CardContainer _cardContainer;
    private GameObject _gameObject;
    private SpriteRenderer _spriteRenderer;

    public Card(string suit, int rank)
    {
        _id = suit + rank.ToString();
        _isShown = false;
        _gameObject = GameObject.Instantiate(GameObject.Find("CardManager").GetComponent<CardManager>().cardPreFab);
        _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        this.UpdateSprite();
    }

    public string GetId()
    {
        return _id;
    }

    public string GetSpriteName()
    {
        return Utility.GetSpriteNameFromCardId(_id);
    }

    public void UpdateSprite()
    {
        if (this._isShown) {
            _spriteRenderer.sprite = Resources.Load<Sprite>(this.GetSpriteName());
        } else {
            _spriteRenderer.sprite = Resources.Load<Sprite>("card_back");
        }
    }

    public void Flip()
    {
        _isShown = !_isShown;
    }

    public void SetCurrentCardContainer(CardContainer container)
    {
        this._cardContainer = container;
    }

    public CardContainer GetCurrentCardContainer()
    {
        return _cardContainer;
    }
}
