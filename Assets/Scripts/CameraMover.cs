using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{ 
    [SerializeField] private Transform _hook;
    [SerializeField]private Vector2 _offset = new Vector2(0, 1f);
    [SerializeField]private float _damping = 1.5f;

    void Start ()
    {
        _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
    }

    void Update () 
    {
        Vector3 target = new Vector3(0, _hook.position.y + _offset.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, _damping * Time.deltaTime);
    }
}
