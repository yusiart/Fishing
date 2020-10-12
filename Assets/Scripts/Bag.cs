using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

[RequireComponent(typeof(Hook))]
public class Bag : MonoBehaviour
{
  [SerializeField] private FishCountDisplay _fishDisplay;
  
  private static int _capacity;
  private HookMover _hook;
  private List<Fish> _fishes = new List<Fish>();
  
  public UnityAction <int, int>  OnFishesCountChanged;


  private void Start()
  {
    _hook = GetComponent<HookMover>();
  }

  private void OnEnable()
  {
    _fishDisplay.GetBag(this);
    OnFishesCountChanged?.Invoke(_fishes.Count, _capacity);
  }

  private void OnDisable()
  {
    _fishDisplay.ResetBag(this);
  }

  public void UpdateFishesBag(int capacity)
  {
    _capacity = capacity;
    OnFishesCountChanged?.Invoke(_fishes.Count, _capacity);
  }

  public bool TryToAddFish(Fish fish)
  {
     if (_fishes.Count < _capacity)
    {
      _fishes.Add(fish);
      fish.transform.SetParent(this.transform);
      OnFishesCountChanged?.Invoke(_fishes.Count, _capacity);
      
      if (_fishes.Count == _capacity)
      {
        //_hook.Accelerate();
       // _hook.EndCachingFishes();
      }
      
      
      return true;
    }
     
    
     //_hook.EndCachingFishes();
    return false;
  }

  public void TryToSellFishes(Player player)
  {
    if (_fishes.Count > 0)
    {
      foreach (var fish in _fishes)
      {
        fish.SellFish(player);
      }
      
      _fishes = new List<Fish>();
      OnFishesCountChanged?.Invoke(_fishes.Count, _capacity);
    }
  }
}
