using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Rod : MonoBehaviour
{
    [SerializeField]private List<Hook> _hooks;
    [SerializeField] private Hook _currentHook;
    [SerializeField] private Transform _origin;
    [SerializeField] private CameraMover _camera;
    
    private bool _isShooting;
    int i = 0;

    private void Start()
    {
        //_camera.SetTarget(_currentHook);
        SetHook();
        if (_currentHook != null)
        {
            SetActiveeHook();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isShooting)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                LaunchHook(_currentHook);
            }
        }
    }

    private void LaunchHook(Hook hook)
    {
        if (hook != null)
        {
            SetActiveeHook();
            _isShooting = true;
            hook.GetComponent<HookMover>().SetTarget();
        }
    }

    private void SetActiveeHook()
    {
        foreach (var hook in _hooks)
        {
           hook.gameObject.SetActive(false);
        }
        
        _currentHook.gameObject.SetActive(true);
    }

    public void Reload()
    {
        _isShooting = false;
    }

    public void SetHook()
    {
        List <Hook> filtredHooks = new List<Hook> (from hook in _hooks
            where hook.IsBuyed 
            select hook);

        if (filtredHooks.Count == 0)
        {
            return;
        }
        
        if (i < filtredHooks.Count - 1)
        {
            i++;
        }
        else
        {
            i = 0;
        }
        
        _currentHook = _hooks[i];
        _camera.GetObject(_currentHook);
        SetActiveeHook();
    }
}