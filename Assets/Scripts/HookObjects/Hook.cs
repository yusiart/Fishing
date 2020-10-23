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

public class Hook : MonoBehaviour
{
   [SerializeField] private int _price;
   [SerializeField] private string _name;
   [SerializeField] private Sprite _icon;
   [SerializeField] private Player _player;
   [SerializeField] private Transform _poolPointContainer;
   [SerializeField] private bool _isBuyed;
   
   private Bag _bag;
   private HookMover _hookMover;
   private SpriteRenderer _renderer;

   public Player Player => _player;

   public event UnityAction OnCloseCollectPanel;

   // event ? ??  kazdij raz izmnenjat' nuzno
   [HideInInspector] public bool Retracting;
   
   public string Name => _name;
   public int Price => _price;
   public bool IsBuyed => _isBuyed;
   public Sprite Icon => _icon;



   private void OnEnable()
   {
      _bag = GetComponent<Bag>();
      _hookMover = GetComponent<HookMover>();
      
      //^^^]]
      //Retracting = _mover.Retracting;

      OnCloseCollectPanel += _hookMover.ReloadRod; 
      _isBuyed = PlayerPrefs.GetInt("IsBuyed") == 1 ? true : false;
      // ResetPoolActivators();
   }
   
   private void OnDisable()
   {
      OnCloseCollectPanel -= _hookMover.ReloadRod;
   }
   
   // private void ResetPoolActivators()
   // {
   //    foreach (PoolActivator child in _poolPointContainer.transform) 
   //    {
   //       child.SetCurrentHook(this);
   //    }
   // }

   public void Unlock()
   {
      _isBuyed = true;
      PlayerPrefs.SetInt("IsBuyed", IsBuyed ? 1 : 0);
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
}