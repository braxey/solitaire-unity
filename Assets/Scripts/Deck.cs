using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    void Awake()
    {
        _spriteRenderer = transform.gameObject.GetComponent<SpriteRenderer>();
        _collider = transform.gameObject.AddComponent<BoxCollider2D>();
    }

    void Update()
    {
    }
}
