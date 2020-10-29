using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookColorChanger : MonoBehaviour
{
    private ColorChanger _colorChanger;
    
    private void Start()
    {
        _colorChanger = FindObjectOfType<ColorChanger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ColorChangePoint>(out ColorChangePoint point))
        {
            _colorChanger.SetTargetColor(point.Color);
        }
    }
}
