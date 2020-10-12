using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Hook : MonoBehaviour
{
   [SerializeField] private int _price;
   [SerializeField] private string _name;
   [SerializeField] private Sprite _icon;
   [SerializeField] private Player _player;
   [SerializeField] private Transform _poolPointContainer;
   
   private Bag _bag;
   private bool _isBuyed;
   private int _capacity = 3;
   private SpriteRenderer _renderer;
   private HookMover _mover;

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
      
      Retracting = _mover.Retracting;
      _bag.UpdateFishesBag(_capacity);
     // ResetPoolActivators();
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

   public void EnlargeCapacity()
   {
      _capacity += 2;
      _bag.UpdateFishesBag(_capacity);
   }
}