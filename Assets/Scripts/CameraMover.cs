using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using UnityEngine;

public class CameraMover : MonoBehaviour
{ 
    [SerializeField]private Vector2 _offset = new Vector2(0, 1f);
    [SerializeField]private float _damping = 1.5f;
    [SerializeField] private List<Fish> _allFishes;

    private Hook _hook;
    private float _outOfRangeYPos = 45f;

    private void Start ()
    {
        Screen.SetResolution (Screen.width, Screen.height, true);
        _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
    }

    private void FixedUpdate()
    {
        if (_hook != null)
        {
            Vector3 target = new Vector3(0, _hook.transform.position.y + _offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, _damping * Time.deltaTime);
        }
    }

    private void Update()
    {
        CheckFishPosition();
    }

    public void SetCurrentHook(Hook hook)
    {
        _hook = hook;
    }
    
    public void AddFish(Fish fish)
    {
        _allFishes.Add(fish);
    }

    private void CheckFishPosition()
    {
        foreach (var fish in _allFishes)
        {
            if (transform.position.y - fish.transform.position.y < -_outOfRangeYPos)
            {
                fish.gameObject.SetActive(false);
            }
            else if(transform.position.y - fish.transform.position.y > _outOfRangeYPos)
            {
                fish.gameObject.SetActive(false);
            }
        }
    }
}
