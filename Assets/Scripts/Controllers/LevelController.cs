using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<Settings> _levels;

    public static LevelController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    /// <summary>
    /// Load specified level
    /// </summary>
    /// <param name="level">Level min value 0</param>
    /// <returns></returns>
    public Settings GetLevel(int level) 
    {
        return _levels[level % _levels.Count];
    }
}

