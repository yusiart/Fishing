using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FishMover))]
public class Fish : MonoBehaviour
{
    [SerializeField] private int _reward;
    
    private Spawner _pool;
    
    public int Reward => _reward;

    private void Start()
    {
        _pool = FindObjectOfType<Spawner>();
    }
    
    public void SellFish(Player player)
    {
        player.SellFish(this);
        transform.SetParent(_pool.transform);
        gameObject.SetActive(false);
    }
}
