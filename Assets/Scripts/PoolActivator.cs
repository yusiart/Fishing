using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

[RequireComponent(typeof(Spawner))]

public class PoolActivator : MonoBehaviour
{
    private Spawner _spawner;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Hook>(out Hook hook))
        {
            _spawner.TryActivateFishes();
        }
    }
}
