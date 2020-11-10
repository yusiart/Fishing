using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rod : MonoBehaviour
{
    [SerializeField]private List<Hook> _hooks;
    [SerializeField] private List<Image> _images;

    private CameraMover _camera;
    private float _depth = -30;
    private Hook _currentHook;
    private bool _isShooting;
    private int _capacity = 5;
    private int _counter;
    private Player _player;
    private int _maxDeepth = -870;

    public Player Player => _player;
    public bool IsShooting => _isShooting;
    public Hook CurrentHook => _currentHook;

    private void Awake()
    {
        LoadBuyedHooks();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("_capacity"))
        {
            _capacity = PlayerPrefs.GetInt("_capacity");
        }
        
        if (PlayerPrefs.HasKey("_deepth"))
        {
            _depth = PlayerPrefs.GetFloat("_deepth");
        }
    }

    private void Start()
    {
        _camera = FindObjectOfType<CameraMover>();

        SetHook();
        
        if (_currentHook != null)
        {
            SetActiveeHook();
        }
        
        _player = _currentHook.Player;
    }

    private void Update()
    {

#if UNITY_ANDROID
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !_isShooting)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
              return;
            }
            else
            {
                LaunchHook(_currentHook);
            }
        }
#endif
        
        if (Input.GetMouseButtonDown(0) && !_isShooting)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                LaunchHook(_currentHook);
            }
        }
    }

    private void LoadBuyedHooks()
    {
        foreach (var hook in _hooks)
        {
            hook.gameObject.SetActive(true);
            hook.CheckBuyedHooks(hook.Number);
            hook.gameObject.SetActive(false);
        }
    }

    private void LaunchHook(Hook hook)
    {
        if (hook != null)
        {
            ResetButtons(false);
            SetActiveeHook();
            _isShooting = true;
            hook.GetComponent<HookMover>().SetTarget(_depth);
        }
    }

    private void SetActiveeHook()
    {
        foreach (var hook in _hooks)
        {
           hook.gameObject.SetActive(false);
        }
        
        _currentHook.gameObject.SetActive(true);
        _currentHook.UpdateFishesBag(_capacity);
    }
    
    public void Reload()
    {
        ResetButtons(true);
        _isShooting = false;
    }

    public void SetHook()
    {
        List <Hook> filtredHooks = new List<Hook> (from hook in _hooks
            where hook.IsBuyed 
            select hook);

        if (filtredHooks.Count == 0)
        {
            return;
        }
        
        if (_counter < filtredHooks.Count - 1)
        {
            _counter++;
        }
        else
        {
            _counter = 0;
        }
        
        _currentHook = filtredHooks[_counter];
        _camera.SetCurrentHook(_currentHook);
        SetActiveeHook();
    }

    private void ResetButtons(bool value)
    {
        foreach (var button in _images)
        {
            button.gameObject.SetActive(value);
        }
    }

    public bool IncreaseDeepLenght(int price)
    {
        if (_player.TryBuyLenght(price))
        {
            if (_depth -20 >= _maxDeepth)
            {
                 _depth -= 20;
                 PlayerPrefs.SetFloat("_deepth", _depth);
                 return true;
            }
            else
            {
                _depth = _maxDeepth;
                PlayerPrefs.SetFloat("_deepth", _depth);
                return true;
            }
        }

        return false;
    }
    
    public bool EnlargeCapacity(int price)
    {
        if (_player.TryBuyCapacity(price))
        {
            _capacity += 1;
            PlayerPrefs.SetInt("_capacity" , _capacity);
            _currentHook.UpdateFishesBag(_capacity);
            return true;
        }

        return false;
    }
}