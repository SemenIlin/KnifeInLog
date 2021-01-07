using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [Range(0.01f, 0.08f)]
    [SerializeField] private float multiplicator = 0.03f;

    private DivisionIntoPartsEffect _divisionIntoPartsEffect;
    private WoodModel _woodModel;
    private GameObject _wood;
    private Transform _woodTransform;


    private float _time;
    private float _iterator;
    private float _newSpeed;

    private void Start()
    {
        _woodModel = _settings.Get();
        _wood = _woodModel.gameObject;
        _divisionIntoPartsEffect = _wood.GetComponent<DivisionIntoPartsEffect> ();
        _divisionIntoPartsEffect.DisableParts();
        _woodTransform = _wood.transform;
        _newSpeed = _woodModel.Speed;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _woodModel.TimerForChangeDirection)
        {
            _time = 0;
            _newSpeed = Random.Range(-10f, 10f);
            Debug.Log(_newSpeed);
            _iterator = 0;
        }

        if (_newSpeed != _woodModel.Speed) 
        {
            _iterator += Time.deltaTime * multiplicator;
            if (_iterator >= 1)
                _iterator = 1;

            _woodModel.Speed = Mathf.Lerp(_woodModel.Speed, _newSpeed, _iterator);
        }

        if (_woodModel.Firmness <= Knifes.Instance.CountKnifesInWood && GameManager.IsGame)
        {
            SoundEffects.Instance.WoodCrackling();

            Knifes.Instance.GetNewParent();
            Knifes.Instance.AddGravity();
            Knifes.Instance.DisactivateCollider();

            _divisionIntoPartsEffect.GetNewParentForWoodParts();
            _divisionIntoPartsEffect.EnableSpriteRendererForParts();
            _divisionIntoPartsEffect.AddForceGravicyParts();
            _wood.SetActive(false);
            GameManager.IsGame = false;
            
            _settings.UpdateValues();
        }
    }
    private void FixedUpdate()
    {
        _woodTransform.Rotate(0f, 0f, _woodModel.Speed);
    }
}
