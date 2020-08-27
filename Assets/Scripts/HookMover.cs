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

    private Vector3 _target;
    private LineRenderer _lineRenderer;
    private bool _retracting;
    private int _capacity;
    
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _collector.GetComponent<FishesCollector>();
    }
    
    private void OnEnable()
    {
        _capacity = _hook.Capacity;
        _collector.StartFishing(false);
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        _lineRenderer.SetPosition(0, _origin.position);
        _lineRenderer.SetPosition(1, transform.position);

        if (_retracting)
            MousePositionFollow();

        if (gameObject.transform.position == _target)
        {
            StartFishing();
        }
        else if ((_origin.position.y - transform.position.y) < 0.8f && _retracting)
        {
            EndFishing();
        }
    }
   
    private void StartFishing()
    {
        _bag.UpdateFishesBag(_capacity);
        _collector.StartFishing(true);
        _retracting = true;
    }

    private void EndFishing()
    {
        _retracting = false;
        _bag.TryToSellFishes(_player);
        _rod.Reload();
        gameObject.SetActive(false);
    }

    private void MousePositionFollow()
    {
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = transform.position;
        position.x = vector.x;
        transform.position = position;
        _target = new Vector3(0f, _origin.transform.position.y, 0f);
    }

    public void SetTarget(Vector3 pos)
    {
        _target = pos;
    }
}
