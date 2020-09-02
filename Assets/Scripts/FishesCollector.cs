using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishesCollector : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    private bool _isFishing = false;
    

    public void ChangeIsFishing(bool isFishing)
    {
        _isFishing = isFishing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Fish>(out Fish fish) && _isFishing)
        {
            if (_bag.TryToAddFish(fish))
            {
                collision.gameObject.GetComponent<FishMover>().Hooked();
            }
        }
    }
}
