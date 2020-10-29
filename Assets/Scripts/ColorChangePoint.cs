using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangePoint : MonoBehaviour
{
    [SerializeField] private Color _color;

    public Color Color => _color;
}