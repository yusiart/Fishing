using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HookView : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _buyButton;

    private Hook _hook;

    public UnityAction<Hook, HookView> SellButtonClick;
    
    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButton);
        _buyButton.onClick.AddListener(TryToLockHook);
    }
    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButton);
        _buyButton.onClick.RemoveListener(TryToLockHook);
    }

    private void TryToLockHook()
    {
        if (_hook.IsBuyed)
        {
            _buyButton.interactable = false;
        }
    }

    public void Render(Hook hook)
    {
        _hook = hook;
        _name.text = hook.Name;
        _price.text = hook.Price.ToString();
        _icon.sprite = hook.Icon;
    }

    public void OnBuyButton()
    {
        SellButtonClick?.Invoke(_hook, this);
    }
}
