﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgrateButton : MonoBehaviour
{
   [SerializeField] private Text _price;
   [SerializeField] private Rod _rod;

   private int price = 20;

   private void Start()
   {
      _price.text = price.ToString();
   }


   public void OnEnlargeLenghtButtonClick()
   {
      if (_rod.IncreaseDeepLenght(price))
      {
         UpdatePrice();
      }
   }

   public void OnIncreaceCapacityButtonClick()
   {
      if (_rod.EnlargeCapacity(price))
      {
        UpdatePrice();
      }
   }

   private void UpdatePrice()
   {
      price += price * 2;
      _price.text = price.ToString();
   }
}
