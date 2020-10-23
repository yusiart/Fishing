using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FishCountDisplay : MonoBehaviour
{
    [SerializeField] private Text _catchedFishes;
    
    private Bag _bag;
    
    private void OnCountChanged(int fishesCount, int capacity)
    {
        _catchedFishes.text = fishesCount + " / " + capacity;
    }
    
    public void GetBag(Bag bag)
    {
        _bag = bag;
        _bag.OnFishesCountChanged += OnCountChanged;
    }

    public void ResetBag(Bag bag)
    {
        bag.OnFishesCountChanged -= OnCountChanged;
    }
}
