using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

[RequireComponent(typeof(Hook))]
[RequireComponent(typeof(FishesCollector))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Bag))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HookColorChanger))]

public class Hook : MonoBehaviour
{
   [SerializeField] private int _price;
   [SerializeField] private string _name;
   [SerializeField] private Sprite _icon;
   [SerializeField] private Player _player;
   [SerializeField] private bool _isBuyed;
   [SerializeField] private int _number;

   private Bag _bag;
   private HookMover _hookMover;
   private SpriteRenderer _renderer;

   public int Number => _number;
   public Player Player => _player;
   public string Name => _name;
   public int Price => _price;
   public bool IsBuyed => _isBuyed;
   public Sprite Icon => _icon;
   
   public event UnityAction OnCloseCollectPanel;
   
   private void OnEnable()
   {
      _bag = GetComponent<Bag>();
      _hookMover = GetComponent<HookMover>();
      OnCloseCollectPanel += _hookMover.ReloadRod;
   }
   
   private void OnDisable()
   {
      OnCloseCollectPanel -= _hookMover.ReloadRod;
   }

   public void UnlockHook(int hookNumber)
   {
      _isBuyed = true;
       PlayerPrefs.SetInt($"IsBuyed{hookNumber}", _isBuyed ? 1 : 0);
       PlayerPrefs.Save();
   }

   public void TryToSellFishes()
   {
      _bag.TryToSellFishes(_player);
   }

   public void CloseCollectPanel()
   {
      OnCloseCollectPanel?.Invoke();
   }

   public void UpdateFishesBag(int capacity)
   {
      _bag.UpdateFishesBag(capacity);
   }

   public void CheckBuyedHooks(int hookNumber)
   {
      if (PlayerPrefs.HasKey($"IsBuyed{hookNumber}"))
      {
         _isBuyed = PlayerPrefs.GetInt($"IsBuyed{hookNumber}") == 1 ? true : false;
      }
   }
}