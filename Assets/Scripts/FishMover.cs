using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishMover : MonoBehaviour
{
  [SerializeField] private bool _isRightSide;
  [SerializeField] private float _moveRangeY;
  [SerializeField] private float _speed;

  private float _moveY;
  private bool _isItCatched;
  private Vector2 _direction;
  private float _rangeSpread;
  private float _axialMovement = 40f;

  private void OnEnable()
  {
    _isItCatched = false;
    _moveY = Random.Range(-_moveRangeY, _moveRangeY);
    _rangeSpread = Random.Range(2, 5);
    _direction = new Vector2( _axialMovement, _moveY);
  }

  private void OnDisable()
  {
    transform.Rotate(0,0,-90);
  }

  private void Update()
  {
    if (!_isItCatched)
    {
      Move();
    }
  }
  
  private void Move()
  {
    transform.position = Vector2.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
    
    if (transform.position.x < -_rangeSpread)
    {
      _direction = ChangeDirection(_axialMovement);
    }
    else if (transform.position.x > _rangeSpread)
    {
      _direction = ChangeDirection(-_axialMovement);
    }
  }

  private Vector2 ChangeDirection(float axialMovement)
  {
    _moveY = Random.Range(-_moveRangeY, _moveRangeY);
    Spin();
    return new Vector2( axialMovement, _moveY);
  }
  
  private void Spin()
  {
    _isRightSide = !_isRightSide;
    transform.Rotate(0f, 180f, 0f);
  }

  public void Hooked()
  {
    transform.Rotate(0,0,90);
    _isItCatched = true;
  }
}