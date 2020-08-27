using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookView : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _buyButton;

    private Hook _hook;
    public void Render(Hook hook)
    {
        
    }
}
