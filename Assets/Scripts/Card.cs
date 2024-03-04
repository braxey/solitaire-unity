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
    private BoxCollider2D _collider;

    public Card(string suit, int rank)
    {
        _id = suit + rank.ToString();
        _isShown = false;
        _gameObject = GameObject.Instantiate(GameObject.Find("CardManager").GetComponent<CardManager>().cardPreFab);
        _gameObject.name = _id;
        _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        _collider = _gameObject.AddComponent<BoxCollider2D>();
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
            _spriteRenderer.sprite = Resources.Load<Sprite>(Utility.CardBackSpriteName);
        }
    }

    public void Flip()
    {
        _isShown = !_isShown;
        this.UpdateSprite();
    }

    public bool IsShown()
    {
        return this._isShown;
    }

    public void SetCurrentCardContainer(CardContainer container)
    {
        this._cardContainer = container;
        this._gameObject.transform.position = container.transform.position;
        // Debug.Log(this._gameObject.transform.position);
        // Debug.Log(container.transform.position);
    }

    public CardContainer GetCurrentCardContainer()
    {
        return _cardContainer;
    }

    public void SetSortOrder(int order)
    {
        this._spriteRenderer.sortingOrder = order;
    }
}
