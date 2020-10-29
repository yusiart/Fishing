using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Rod _rod;
    private Player _player;

    private void Start()
    {
        _rod = FindObjectOfType<Rod>();
        _player = FindObjectOfType<Player>();
    }

    public void OnChangeHookButtonClick()
    {
        if (_rod.IsShooting == false)
        {
            _rod.SetHook();
        }
    }

    public void OnCollectButtonClick(bool x2Collect)
    {
        _player.ChangeMoneyDisplay(x2Collect);
        StartCoroutine(WaitForClosePanel());
    }
    
    IEnumerator WaitForClosePanel()
    {
        yield return new WaitForSeconds(0.5f);
        
        _rod.CurrentHook.CloseCollectPanel();
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
