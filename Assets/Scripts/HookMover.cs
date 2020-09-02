using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMover : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private float _speed;
    [SerializeField] private FishesCollector _collector;
    [SerializeField] private Rod _rod;
    [SerializeField] private Bag _bag;
    [SerializeField] private Player _player;
    [SerializeField] private Hook _hook;
    [SerializeField] private float _depth;

    private Vector3 _target;
    private LineRenderer _lineRenderer;
    private bool _retracting;
    private bool _canMove;
    private float _accelerationCount = 2;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _collector.GetComponent<FishesCollector>();
    }
    
    private void OnEnable()
    {
        _hook.SetCapacity(_bag);
        _collector.ChangeIsFishing(false);
    }
    
    private void Update()
    {
        if (_canMove)
        {
            Move();
        }

        if (_retracting)
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
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        _lineRenderer.SetPosition(0, _origin.position);
        _lineRenderer.SetPosition(1, transform.position);
    }

    private void StartFishing()
    {
        _collector.ChangeIsFishing(true);
        _retracting = true;
        ResetSpeed();
    }

    private void EndFishing()
    {
        _retracting = false;
        _bag.TryToSellFishes(_player);
        _rod.Reload();
        _collector.ChangeIsFishing(false);
        _canMove = false;
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

    public void Accelerate()
    {
        _speed *= _accelerationCount;
    }

    public void ResetSpeed()
    {
        _speed /= _accelerationCount;
    }
}
