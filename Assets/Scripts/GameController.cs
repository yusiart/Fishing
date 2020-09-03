using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Rod _rod;

    public void OnChangeHookButtonClick()
    {
        if (_rod.IsShooting == false)
        {
            _rod.SetHook();
        }
    }
}
