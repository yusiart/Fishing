using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class FishMover : MonoBehaviour
{
  [SerializeField] private float _speed;

  private bool _isRightSide;
  private bool _isItCatched;
  private Quaternion _startTransform;
  private Vector2 _direction;
  private float _rangeSpread;
  private readonly float _axialMovement = 40f;

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
        Move();
  }

  private void SetStartSettings()
  {
    transform.rotation = _startTransform;
    _isRightSide = true;
    _isItCatched = false;
    _rangeSpread = Random.Range(3, 6);
    _direction = new Vector2(_axialMovement, transform.position.y);
  }

  private void Move()
  {
    transform.position = Vector2.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);

    CheckXPosition();
  }

  private void CheckXPosition()
  {
    if (transform.position.x < -_rangeSpread && !_isRightSide)
      _direction = ChangeDirection(_axialMovement);
    else if (transform.position.x > _rangeSpread && _isRightSide) _direction = ChangeDirection(-_axialMovement);
  }

  private Vector2 ChangeDirection(float axialMovement)
  {
    Spin();
    return new Vector2(axialMovement, transform.position.y);
  }

  private void Spin()
  {
    _isRightSide = !_isRightSide;
    transform.Rotate(0f, 180f, 0f);
  }

  public void Hooked()
  {
    transform.Rotate(0, 0, 90);
    _isItCatched = true;
  }
}