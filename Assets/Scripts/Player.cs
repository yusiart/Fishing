using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

public class Player : MonoBehaviour
{
   [SerializeField] private int _money;

   private int _sumReward;
   private InitilizeAds _ads;
   private CollectText _text;

   public int Money
   {
      get => _money;
   }

   public event UnityAction<int> OnMoneyChanged;


   private void Start()
   {
      _text = GetComponent<CollectText>();
      _ads = GetComponent<InitilizeAds>();
      
      if (PlayerPrefs.HasKey("_money"))
      {
         _money = PlayerPrefs.GetInt("_money");
      }
      
      OnMoneyChanged?.Invoke(_money);
   }

   public void SellFish(Fish fish)
   {
      if (fish.Reward > 0)
      {
         _sumReward += fish.Reward;
      }
   }

   public void ChangeMoneyDisplay(bool collectX3)
   {
      if (collectX3)
      {
         _ads.ShowInterstitialAd();
         _money += _sumReward * 3;
      }
      else
      {
         _money += _sumReward;
      }
      
      OnMoneyChanged?.Invoke(_money);
      _sumReward = 0;
   }

   public void BuyHook(Hook hook, bool buyForAds)
   {
      if (!buyForAds)
      {
         _money -= hook.Price;
         OnMoneyChanged?.Invoke(_money);
      }
      
      hook.UnlockHook(hook.Number);
   }

   public bool TryBuyLenght(int price)
   {
      if (TrySpendMoney(price))
      {
         return true;
      }
      
      return false;
   }

   public bool TryToBuyCapacity(int price)
   {
      if (TrySpendMoney(price))
      {
         return true;
      }
      
      return false;
   }
   public void SetText()
   {
      _text.SetText(_sumReward);
   }

   public void SaveMoney()
   {
      PlayerPrefs.SetInt("_money", _money);
   }

   private bool TrySpendMoney(int price)
   {
      if (_money >= price)
      {
         _money -= price;
         OnMoneyChanged?.Invoke(_money);
         return true;
      }

      return false;
   }
}
