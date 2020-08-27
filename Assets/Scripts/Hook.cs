using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hook : MonoBehaviour
{
   [SerializeField] private int _capacity;
   [SerializeField] private bool _isBuyed = false;
   [SerializeField] private int _price;
   [SerializeField] private string _name;
   [SerializeField] private Image _icon;

   public string Name => _name;
   public int Price => _price;
   public bool IsBuyed => _isBuyed;
   public Image Icon => _icon;

   public int Capacity => _capacity;
}