using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
  [SerializeField] private HookMover _hook;
  [SerializeField] private FishCountDisplay _fishDisplay;
  
  private int _capacity;
  private List<Fish> _fishes = new List<Fish>();
  
  public UnityAction <int, int>  OnFishesCountChanged;

  private void OnEnable()
  {
    _fishDisplay.GetBag(this);
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
        _hook.Accelerate();
      }
      
      return true;
    }

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
