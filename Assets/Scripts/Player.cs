using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
   [SerializeField] private int _money;

   private int _sumReward;

   public int Money
   {
      get => _money;
   }

   public event UnityAction<int> OnMoneyChanged;

   private void Start()
   {
      OnMoneyChanged?.Invoke(_money);
   }

   public void SellFish(Fish fish)
   {
      if (fish.Reward > 0)
      {
         _sumReward += fish.Reward;
         OnMoneyChanged?.Invoke(_money);
      }
   }

   public void ChangeMoneyDisplay(bool collectX3)
   {
      if (collectX3)
      {
          _money += _sumReward * 3;
      }
      else
      {
         _money += _sumReward;
      }
      
      OnMoneyChanged?.Invoke(_money);
      _sumReward = 0;
   }

   public void BuyHook(Hook hook)
   {
      _money -= hook.Price;
      OnMoneyChanged?.Invoke(_money);
      hook.Buy();
   }
}