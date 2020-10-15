using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

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
   private HookMover _mover;
   
   public event UnityAction OnClosePanel;

   // event ? ??  kazdij raz izmnenjat' nuzno
   [HideInInspector] public bool Retracting;
   
   public string Name => _name;
   public int Price => _price;
   public bool IsBuyed => _isBuyed;
   public Sprite Icon => _icon;



   private void OnEnable()
   {
      _mover = GetComponent<HookMover>();
      _bag = GetComponent<Bag>();
      _hookMover = GetComponent<HookMover>();
      
      Retracting = _mover.Retracting;

      OnClosePanel += _hookMover.ReloadRod;
      // ResetPoolActivators();
   }
   
   private void OnDisable()
   {
      OnClosePanel -= _hookMover.ReloadRod;
   }
   
   // private void ResetPoolActivators()
   // {
   //    foreach (PoolActivator child in _poolPointContainer.transform) 
   //    {
   //       child.SetCurrentHook(this);
   //    }
   // }

   public void Buy()
   {
      _isBuyed = true;
   }

   public void TryToSellFishes()
   {
      _bag.TryToSellFishes(_player);
   }

   public void CloseCollectPanel()
   {
      OnClosePanel?.Invoke();
   }

   public void UpdateFishesBag(int capacity)
   {
      _bag.UpdateFishesBag(capacity);
   }
}