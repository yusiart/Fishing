using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Hook))]
[RequireComponent(typeof(FishesCollector))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Bag))]
[RequireComponent(typeof(Rigidbody2D))]

public class HookMover : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private Rod _rod;
    [SerializeField] private GameObject _collectPanel;

    private float _startSpeed = 8f;
    private FishesCollector _collector;
    private Hook _hook;
    private Vector3 _target;
    private LineRenderer _lineRenderer;
    private bool _retracting;
    private bool _canMove;
    private float _accelerationCount = 4;
    private float _currentSpeed;
    private bool _isBagSpaceEnough;

    public bool Retracting => _retracting;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _hook = GetComponent<Hook>();
    }
    
    private void OnEnable()
    {
        _collector = GetComponent<FishesCollector>();
        _currentSpeed = _startSpeed;
        _collector.ChangeIsFishing(false);
    }

    private void Update()
    {
        if (_canMove)
            Move();
        
        if (_retracting && _isBagSpaceEnough)
            MousePositionFollow();

        if (gameObject.transform.position ==_target && !_retracting)
        {
            StartFishing();
        }
        else if ((_target.y == transform.position.y) && _retracting)
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
        _collectPanel.SetActive(true);
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
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = transform.position;
        position.x = vector.x;
        transform.position = position;
        _target = new Vector3(0f, _origin.transform.position.y, 0f);
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // ne rabotaet stolknovenie
    //     if (collision.gameObject.TryGetComponent<Player>(out Player player))
    //     {
    //         EndCachingFishes();
    //     }
    // }

    public void ReloadRod()
    {
        _rod.Reload();
        _collectPanel.SetActive(false);
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
