using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hook))]
[RequireComponent(typeof(FishesCollector))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Bag))]

public class HookMover : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private float _startSpeed;
    [SerializeField] private Rod _rod;
    [SerializeField] private float _depth;

    private FishesCollector _collector;
    private Hook _hook;
    private Vector3 _target;
    private LineRenderer _lineRenderer;
    private bool _retracting;
    private bool _canMove;
    private float _accelerationCount = 3;
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
        {
            Move();
        }

        if (_retracting && _isBagSpaceEnough)
            MousePositionFollow();

        if (gameObject.transform.position == _target)
        {
            StartFishing();
        }
        else if ((_origin.position.y - transform.position.y) < 0.3f && _retracting)
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
        _rod.Reload();
        _canMove = false;
        ChangeFishingValues(false);
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

    public void SetTarget()
    {
        Vector2 down = new Vector2(0, _depth);
        _target = down;
        _canMove = true;
        Accelerate();
    }

    public void EndCachingFishes()
    {
        _isBagSpaceEnough = false;
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

    public void IncreaseDeepLenght()
    {
        _depth -= 20;
    }
}
