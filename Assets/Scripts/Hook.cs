using System;
using UnityEngine;

public class Hook : MonoBehaviour
{
   [SerializeField] private int _capacity;
   [SerializeField] private bool _isBuyed;
   [SerializeField] private int _price;
   [SerializeField] private string _name;
   [SerializeField] private Sprite _icon;
   [SerializeField] private Player _player;
   [SerializeField] private Bag _bag;

   private SpriteRenderer _renderer;

   public string Name => _name;
   public int Price => _price;
   public bool IsBuyed => _isBuyed;
   public Sprite Icon => _icon;


   private void OnEnable()
   {
      _bag.UpdateFishesBag(_capacity);
   }

   public void Buy()
   {
      _isBuyed = true;
   }

   public void TryToSellFishes()
   {
      _bag.TryToSellFishes(_player);
   }
}