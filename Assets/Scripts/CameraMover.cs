using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using UnityEngine;

public class CameraMover : MonoBehaviour
{ 
    [SerializeField] private Hook _hook;
    [SerializeField]private Vector2 _offset = new Vector2(0, 1f);
    [SerializeField]private float _damping = 1.5f;

    void Start ()
    {
        _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
    }

    void Update()
    {
        if (_hook != null)
        {
            Vector3 target = new Vector3(0, _hook.transform.position.y + _offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, _damping * Time.deltaTime);
        }
    }

    public void GetObject(Hook hook)
    {
        _hook = hook;
    }
}
