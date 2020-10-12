using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

[RequireComponent(typeof(Spawner))]

public class PoolActivator : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private Hook _currentHook;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Hook>(out Hook hook))// && _currentHook.Retracting == false)
        {
            _spawner.TryActivateFishes();
        }
    }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.TryGetComponent<Hook>(out Hook hook))// && _currentHook.Retracting)
    //     {
    //         _spawner.OffFihses();
    //     }
    // }

    public void SetCurrentHook(Hook hook)
    {
        _currentHook = hook;
    }
}
