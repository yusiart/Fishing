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

    private Hook _hook;
    //private float _outOfRangeYPos = 10f;
    // [SerializeField] private List<Fish> _allFishes = new List<Fish>();

    void Start ()
    {
        Screen.SetResolution (Screen.width, Screen.height, true);
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

    // private void CheckFish()
    // {
    //     for(int i = 0; i < _fishes.Count; i++)
    // {
    //    if(_fishes[i].transform.position.y > 20 \\ _fishes[i].transform.position < 20)
    //     fish.gameobject.SetActive(false);
    // }
    // }
    //
    // private void FindAllFishes()
    // {
    //     Fish fish;
    //     
    //     while (fish = FindObjectOfType<Fish>())
    //     {
    //         _allFishes.Add(fish);
    //     }
    // }
}
