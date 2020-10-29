using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private float _speedColorChange = 1f;
    private SpriteRenderer _spriteRenderer;
    private Color _targetColor = Color.blue;
    private Color _colorAlpha;
    private Color _color;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colorAlpha.a = _spriteRenderer.color.a;
        _targetColor.a = _colorAlpha.a;
    }

    private void Update()
    {
        if (_spriteRenderer.color != _targetColor)
        {
            ChangeBackgroundColor();
        }
    }

    private void ChangeBackgroundColor()
    {
        _color = Color.Lerp(_spriteRenderer.color, _targetColor, Time.deltaTime * _speedColorChange);
        _spriteRenderer.color = _color;
    }

    public void SetTargetColor(Color color)
    {
        _targetColor = color;
        _targetColor.a = _colorAlpha.a;
    }
}