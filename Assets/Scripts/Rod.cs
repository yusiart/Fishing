using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    [SerializeField] private HookMover _hook;
    [SerializeField] private int _depth;
    
    private bool _isShooting;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isShooting) 
            LaunchHook();
    }

    private void LaunchHook()
    {
        _isShooting = true;
        Vector2 down = new Vector2(0, _depth);
        _hook.SetTarget(down);
        _hook.gameObject.SetActive(true);
    }
    
    public void Reload()
    {
        _isShooting = false;
    }

    public void IncreaseLength()
    {
        _depth = _depth - 20;
    }
}