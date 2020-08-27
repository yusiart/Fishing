using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
  private int _capacity;
  private List<Fish> _fishes = new List<Fish>();

  public List<Fish> Fishes
  {
    get => _fishes;
  }

  public void UpdateFishesBag(int capacity)
  {
    _capacity = capacity;
  }

  public bool TryToAddFish(Fish fish)
  {
     if (_fishes.Count < _capacity)
    {
      _fishes.Add(fish);
      fish.transform.SetParent(this.transform);
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
        player.SellFish(fish);
        fish.transform.SetParent(null);
        fish.ResetFish();
        fish.enabled = false;
      }
      _fishes = new List<Fish>();
    }
  }
}
