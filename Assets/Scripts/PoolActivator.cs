using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolActivator : MonoBehaviour
{
    [SerializeField] private GameObject _pool;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Hook>(out Hook hook))
        {
            _pool.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Hook>(out Hook hook))
        {
            _pool.SetActive(false);
        }
        else if (other.gameObject.TryGetComponent<FishMover>(out FishMover fish))
        {
            Debug.Log("1");
            fish.ChangeYDirection();
        }
    }
}
