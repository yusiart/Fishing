using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class FishesCollector : MonoBehaviour
{
    private Bag _bag;
    private bool _isFishing = true;

    private void OnEnable()
    {
        _bag = GetComponent<Bag>();
    }

    public void ChangeIsFishing(bool isFishing)
    {
        _isFishing = isFishing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Fish>(out Fish fish) &&  _isFishing)
        {
            if (_bag.TryToAddFish(fish))
            {
                collision.gameObject.GetComponent<FishMover>().Hooked();
            }
        }
    }
}
