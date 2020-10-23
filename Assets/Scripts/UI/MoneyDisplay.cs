using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _moneyText;

    private void OnEnable()
    {
        _player.OnMoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int gold)
    {
        _moneyText.text = gold.ToString();
        _player.SaveMoney();
    }
}
