using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

public class Bag : MonoBehaviour
{
  [SerializeField] private FishCountDisplay _fishDisplay;

  private static int _fishCapacity;
  private HookMover _hookMover;
  private List<Fish> _cachedFishes = new List<Fish>();

  public UnityAction<int, int> OnFishesCountChanged;

  private void Start()
  {
    _hookMover = GetComponent<HookMover>();
  }

  private void OnEnable()
  {
    _fishDisplay.SetBag(this);
    OnFishesCountChanged?.Invoke(_cachedFishes.Count, _fishCapacity);
  }

  private void OnDisable()
  {
    _fishDisplay.ResetBag(this);
  }

  public void UpdateFishesBag(int capacity)
  {
    _fishCapacity = capacity;
    OnFishesCountChanged?.Invoke(_cachedFishes.Count, _fishCapacity);
  }

  public bool TryToAddFish(Fish fish)
  {
    if (_cachedFishes.Count < _fishCapacity)
    {
      _cachedFishes.Add(fish);
      fish.transform.position = new Vector2(transform.position.x, transform.position.y - 1.1f);
      fish.transform.SetParent(transform);
      OnFishesCountChanged?.Invoke(_cachedFishes.Count, _fishCapacity);

      if (_cachedFishes.Count == _fishCapacity)
      {
        _hookMover.EndCachingFishes();
      }

      return true;
    }

    return false;
  }

  public void TryToSellFishes(Player player)
  {
    if (_cachedFishes.Count > 0)
    {
      foreach (var fish in _cachedFishes)
      {
        fish.SellFish(player);
      }

      _cachedFishes = new List<Fish>();
      OnFishesCountChanged?.Invoke(_cachedFishes.Count, _fishCapacity);
    }
  }
}
