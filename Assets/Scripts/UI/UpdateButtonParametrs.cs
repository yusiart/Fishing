using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class UpdateButtonParametrs : MonoBehaviour
{
    [SerializeField] private Text _price;
    [SerializeField] private int price;
    [SerializeField] private int _number;

    private Rod _rod;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _rod = FindObjectOfType<Rod>();
        
        if (PlayerPrefs.HasKey($"price{_number}"))
        {
            price = PlayerPrefs.GetInt($"price{_number}");
        }

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
        price = (int)(price * 1.5f);
        _price.text = price.ToString();
        PlayerPrefs.SetInt($"price{_number}", price);
    }
}