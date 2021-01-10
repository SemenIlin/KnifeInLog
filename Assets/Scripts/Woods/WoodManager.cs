using System;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    private Settings _settings;
    private DivisionIntoPartsEffect _divisionIntoPartsEffect;
    private WoodModel _woodModel;
    private GameObject _wood;
    private Transform _woodTransform;

    private float _time;
    private float _iterator;

    private bool isHalfPeriod = false;
    private float _newSpeed;
    private float _oldSpeed;
    private float _currentSpeed;
    private int _period = 2;

    public const float TIMER_FOR_CREATE_NEW_WOOD = 1f;
    /// <summary>
    /// Create knife if wood is exist.
    /// </summary>
    public static bool IsCreatedWood { get; private set; }

    /// <summary>
    /// Create knife with wood when player go to next level.
    /// </summary>
    public static event Action CreateKnife;
    public static event Action FailApple;
    public static event Action UpdateCanvas;
    private void Start()
    {
        ButtonManager.Instance.CreateEnviromentLevel += InitWood;
        ButtonManager.Instance.DestroyWood += DestroyWood;
    }
    private void Update()
    {
        if (_settings == null)
            return;

        _settings.UpdateValues();

        _time += Time.deltaTime;
        if (_time >= _woodModel.TimerForChangeDirection)
        {
            RotateWithTimer();
        }

        if (_woodModel.Firmness <= Knifes.Instance.CountKnifesInWood &&
            GameManager.IsGame)
            ChangeWood();

     }
    private void FixedUpdate()
    {
        if (_woodTransform == null)
            return;

        if(GameManager.IsGame)
            _woodTransform.Rotate(0f, 0f, _currentSpeed);
    }

    public void ChangeWood()
    {
        DestroyOnPartWood();
        Invoke("InitWood", TIMER_FOR_CREATE_NEW_WOOD);
        Invoke("CreateKnifeMethod", TIMER_FOR_CREATE_NEW_WOOD);
    }

    public void ResetWood()
    {
        DestroyWood();
        Invoke("InitWood", TIMER_FOR_CREATE_NEW_WOOD);
    }

    private void CreateKnifeMethod()
    {
        CreateKnife?.Invoke();
    }
    private void InitWood()
    {
        IsCreatedWood = true;
        TextController.Instance.ShowLevel();
        _settings = LevelController.Instance.GetLevel(GameManager.Level);
        _woodModel = _settings.Get(transform);
        _wood = _woodModel.gameObject;
        _divisionIntoPartsEffect = _wood.GetComponent<DivisionIntoPartsEffect>();

        _divisionIntoPartsEffect.DisableParts();
        _woodTransform = _wood.transform;
        _currentSpeed = _newSpeed = _woodModel.Speed;

        UpdateCanvas?.Invoke();
    }
    private void DestroyOnPartWood()
    {
        KnifeThrower.IsStartToknifeThrower = false;
        IsCreatedWood = false;
           ++GameManager.Level;
        GameManager.MaxLevel = GameManager.MaxLevel > GameManager.Level ?
            GameManager.MaxLevel :
            GameManager.Level;
        SoundEffects.Instance.WoodCrackling();

        FailApple?.Invoke();
        Knifes.Instance.DisactivateCollider();
        Knifes.Instance.GetNewParent();
        Knifes.Instance.AddGravity();
        Knifes.Instance.DeleteAllKnife();

        _divisionIntoPartsEffect.GetNewParent();
        _divisionIntoPartsEffect.EnableSpriteRendererForParts();
        _divisionIntoPartsEffect.AddForceGravicyParts();

        Destroy(_wood.gameObject);
    }
    private void DestroyWood()
    {
        KnifeThrower.IsStartToknifeThrower = false;
        IsCreatedWood = false;
        Knifes.Instance.DeleteAllKnife();

        Destroy(_wood.gameObject);
    }
    private void RotateWithTimer()
    {        
        _oldSpeed = _woodModel.Speed;
        _newSpeed = 0;
        if (!isHalfPeriod)
        {
            _iterator += Time.deltaTime;
            _iterator = _iterator >= _settings.GetTimeForStopWood ? _settings.GetTimeForStopWood : _iterator;
            if (_period == 2)
            {
                _currentSpeed = Mathf.Lerp(_oldSpeed, _newSpeed, _iterator / _settings.GetTimeForStopWood);
                isHalfPeriod = _currentSpeed == _newSpeed;
            }
            else if (_period == 1)
            {
                _currentSpeed = Mathf.Lerp(_newSpeed, _oldSpeed, _iterator / _settings.GetTimeForStopWood);
                isHalfPeriod = _currentSpeed == _oldSpeed;
            }
        }
        else if (isHalfPeriod)
        {
            --_period;
            _iterator = 0;
            isHalfPeriod = false;
        }

        if (_period == 0)
        {
            _time = 0;
            _period = 2;
        }        
    }
}
