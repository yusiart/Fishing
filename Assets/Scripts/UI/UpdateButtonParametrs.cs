using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateButtonParametrs : MonoBehaviour
{
    [SerializeField] private Text _price;
    [SerializeField] private Rod _rod;

    private int price = 20;

    private void Start()
    {
        price = PlayerPrefs.GetInt("price");
        _price.text = price.ToString();
    }
    

    public void OnIncreaceCapacityButtonClick()
    {
      
        if (_rod.EnlargeCapacity(price))
        {
            UpdatePrice();
        }
    }
    
    public void OnEnlargeLenghtButtonClick()
    {
        if (_rod.IncreaseDeepLenght(price))
        {
            UpdatePrice();
        }
    }

    private void UpdatePrice()
    {
        price = (int)(price * 1.8f);
        _price.text = price.ToString();
        PlayerPrefs.SetInt("price", price);
    }
}
