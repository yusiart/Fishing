using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Hook> _hooks;
    [SerializeField] private GameObject _container;
    [SerializeField] private HookView _template;
    [SerializeField] private Player _player;
    [SerializeField] private Rod _rod;
    
    private void Start()
    {
        for (int i = 0; i < _hooks.Count; i++)
        {
            AddHook(_hooks[i]);
        }
    }

    private void AddHook(Hook hook)
    {
        var item = Instantiate(_template, _container.transform);
        item.SellButtonClick += OnSellButtonClick;
        item.Render(hook);
    }
    

    private void OnSellButtonClick(Hook hook, HookView view, bool buyForAds)
    {
        TryToSellHook(hook, view, buyForAds);
    }

    private void TryToSellHook(Hook hook, HookView view, bool buyForAds)
    {
        if (buyForAds)
        {
            BuyHook(hook, view,buyForAds);
        }
        else if (_player.Money >= hook.Price)
        {
            BuyHook(hook, view,buyForAds);
        }
    }

    private void BuyHook(Hook hook, HookView view, bool buyForAds)
    {
        _player.BuyHook(hook, buyForAds);
        _rod.SetHook();
        view.SellButtonClick -= OnSellButtonClick;
    }
}
