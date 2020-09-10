using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishMover : MonoBehaviour
{
  [SerializeField] private float _moveRangeY;
  [SerializeField] private float _speed;
 
  private bool _isRightSide;
  private bool _isItCatched;
  private Quaternion _startTransform;
  private Vector2 _direction;
  private float _moveY;
  private float _rangeSpread;
  private float _axialMovement = 40f;
  private Transform _targetTransform;

  private void OnEnable()
  {
   SetStartSettings();
  }

  private void Start()
  {
    _speed += Random.Range(0.2f, 1f);
  }

  private void Update()
  {
    if (!_isItCatched)
    {
      Move();
    }
  }

  private void SetStartSettings()
  {
    transform.rotation = _startTransform;
    _isRightSide = true;
    _isItCatched = false;
    _moveY = Random.Range(-_moveRangeY, _moveRangeY);
    _rangeSpread = Random.Range(3, 6);
    _direction = new Vector2( _axialMovement, _moveY);
  }

  private void Move()
  {
    transform.position = Vector2.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);

    CheckXPosition();
    CheckYPosition();
  }

  private void CheckXPosition()
  {
    if (transform.position.x < -_rangeSpread && !_isRightSide)
    {
      _direction = ChangeDirection(_axialMovement);
    }
    else if (transform.position.x > _rangeSpread && _isRightSide)
    {
      _direction = ChangeDirection(-_axialMovement);
    }
  }

  private void CheckYPosition()
  {
    if (transform.position.y > _targetTransform.position.y + 10f)
    {
      Debug.Log("changeY1 proverka");
      StartCoroutine(ResetSpeed());
    }
    else if (transform.position.y < _targetTransform.position.y -10f)
    {
      Debug.Log("changeY2 proverka ");
      StartCoroutine(ResetSpeed());
    }
  }

  private Vector2 ChangeDirection(float axialMovement)
  {
    _moveY += Random.Range(-_moveRangeY, _moveRangeY);
    Spin();
    return new Vector2( axialMovement, transform.position.y + _moveY);
  }
  
  private void Spin()
  {
    _isRightSide = !_isRightSide;
    transform.Rotate(0f, 180f, 0f);
  }
  
  private IEnumerator ResetSpeed()
  {
    ChangeYDirection();
    yield return new WaitForSeconds(2f);

    Debug.Log("caroutine");
  }

  public void Hooked()
  {
    transform.Rotate(0,0,90);
    _isItCatched = true;
  }

  public void ChangeYDirection()
  {
    Debug.Log("Changed");
    _moveY = _moveY * -1;
  }

  public void SetTransform(Transform spawnerTransform)
  {
    _targetTransform = spawnerTransform;
  }
}