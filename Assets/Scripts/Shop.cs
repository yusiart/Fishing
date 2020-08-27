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
        
        item.Render(hook);
    }
}
