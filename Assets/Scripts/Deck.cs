using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private GameObject _holder;
    [SerializeField] private CardContainer _container;

    void Awake()
    {
        transform.position = _holder.transform.position;
    }

    void Update()
    {
    }
}
