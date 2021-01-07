using System;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    private WoodModel wood;

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
    }

    [SerializeField]
    private WoodConfig _woodConfig;

    public WoodModel Get()
    {
        wood = Instantiate(_woodConfig.Prefab);

        wood.Initialize(_woodConfig.Speed,
                        _woodConfig.Firmness,
                        _woodConfig.ChangeCreateApple,
                        _woodConfig.TimerForChangeDirection);

        return wood;
    }

    public void UpdateValues()
    {
        _woodConfig.Speed = wood.Speed;

        if (wood.Speed != _woodConfig.Speed ||
            wood.TimerForChangeDirection != _woodConfig.TimerForChangeDirection ||
            wood.Firmness != _woodConfig.Firmness ||
            wood.ChangeCreateApple != _woodConfig.ChangeCreateApple)

        wood.Initialize(_woodConfig.Speed,
                        _woodConfig.Firmness,
                        _woodConfig.ChangeCreateApple,
                        _woodConfig.TimerForChangeDirection);
    }
}
