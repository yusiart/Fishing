using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCountDisplay : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private Text _catchedFishes;

    private void OnEnable()
    {
        _bag.OnFishesCountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _bag.OnFishesCountChanged -= OnCountChanged;
    }

    private void OnCountChanged(int fishesCount, int capacity)
    {
        _catchedFishes.text =  fishesCount + " / " + capacity;
    }
}
