using System;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    private WoodModel wood;
    private IDestroy destroy;

     [Serializable]
    class WoodConfig
    {
        public WoodModel Prefab;

        [Range(-10f, 10f)]
        public float Speed = 3f;

        [Range(1, 20)]
        public int Firmness = 6;

        [Range(0f, 100f)]
        public float ChangeCreateApple = 25;

        [Range(3f, 20f)]
        public float TimerForChangeDirection = 5f;

        [Range(0.5f, 5f)]
        public float TimeForStopWood = 2f;

        [Range(0.2f, 10f)]
        public float TimerForCreateKnife = 1f;
    }

    [SerializeField]
    private WoodConfig _woodConfig;

    public WoodModel Get(Transform parrentTransform)
    {
        wood = Instantiate(_woodConfig.Prefab, parrentTransform);
        IDestroy destroy = wood.GetComponent<IDestroy>();

        wood.Initialize(_woodConfig.Speed,
                        _woodConfig.Firmness,
                        _woodConfig.ChangeCreateApple,
                        _woodConfig.TimerForChangeDirection,
                        destroy);

        return wood;
    }

    public float ChangeCreateApple { get { return _woodConfig.ChangeCreateApple; } }
    public float GetTimeForStopWood { get { return _woodConfig.TimeForStopWood; } }
    public int GetFirmness { get { return _woodConfig.Firmness; } }
    
    public float TimerForCreateKnife { get { return _woodConfig.TimerForCreateKnife; } }

    public void UpdateValues()
    {
        wood.Initialize(_woodConfig.Speed,
                        _woodConfig.Firmness,
                        _woodConfig.ChangeCreateApple,
                        _woodConfig.TimerForChangeDirection,
                        destroy);
    }
}
