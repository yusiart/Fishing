using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HookMover : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private GameObject _endFishingCollectPanel;

    private Rod _rod;
    private float _startSpeed = 8f;
    private FishesCollector _collector;
    private Hook _hook;
    private Vector3 _target;
    private LineRenderer _lineRenderer;
    private float _accelerationCount = 6;
    private float _currentSpeed;
    private bool _retracting;
    private bool _canMove;
    private bool _isBagSpaceEnough;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _hook = GetComponent<Hook>();
        _currentSpeed = _startSpeed;
        _collector = GetComponent<FishesCollector>();
        _rod = FindObjectOfType<Rod>();
    }

    private void OnEnable()
    {
        _collector.ChangeIsFishing(false);
    }

    private void FixedUpdate()
    {
        if (_canMove)
            Move();

        if (_retracting && _isBagSpaceEnough)
            MousePositionFollow();
    }

    private void Update()
    {
        if (Math.Abs(gameObject.transform.position.y - _target.y) < 0.01f && !_retracting)
        {
            StartFishing();
        }
        else if ((Math.Abs(_origin.transform.position.y - transform.position.y) < 0.1f) && _retracting)
        {
            EndFishing();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _currentSpeed * Time.deltaTime);
        _lineRenderer.SetPosition(0, _origin.position);
        _lineRenderer.SetPosition(1, transform.position);
    }

    private void StartFishing()
    {
        ChangeFishingValues(true);
    }

    private void EndFishing()
    {
        _hook.TryToSellFishes();
        _canMove = false;
        ChangeFishingValues(false);
        _endFishingCollectPanel.SetActive(true);
        _rod.Player.SetText();
        _target = new Vector3(0, 0, 0);
    }

    private void ChangeFishingValues(bool value)
    {
        _collector.ChangeIsFishing(value);
        _retracting = value;
        _isBagSpaceEnough = value;

        ResetSpeed();
    }

    private void MousePositionFollow()
    {
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 poss = transform.position;
            poss.x = touchPos.x;
            transform.position = poss;
        }

        _target = _origin.transform.position;
#endif

        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = transform.position;
        position.x = vector.x;
        transform.position = position;

        _target = _origin.transform.position;
    }

    public void ReloadRod()
    {
        _rod.Reload();
        _endFishingCollectPanel.SetActive(false);
    }

    public void SetTarget(float depth)
    {
        _target = new Vector2(0, depth);
        _canMove = true;
        Accelerate();
    }

    public void EndCachingFishes()
    {
        _isBagSpaceEnough = false;
        Accelerate();
        _target = _origin.transform.position;
    }

    public void Accelerate()
    {
        _currentSpeed *= _accelerationCount;
    }

    public void ResetSpeed()
    {
        _currentSpeed = _startSpeed;
    }
}
