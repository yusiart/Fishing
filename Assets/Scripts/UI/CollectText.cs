using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectText : MonoBehaviour
{
  [SerializeField] private Text _text;


  public void SetText(int collectedMoney)
  {
    _text.text = "You Collected - " + collectedMoney.ToString();
  }
}
