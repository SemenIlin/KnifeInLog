using UnityEngine;
public class GameValues : MonoBehaviour
{
    public static GameValues Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public bool IsGame { get; private set; }
    public int QuantityApple { get; private set; }
    public int Score { get; private set; }
    public int Level { get; private set; }
    public int MaxScore { get; private set; }
    public int MaxLevel { get; private set; }

    public void SetStatusGame(bool status)
    {
        IsGame = status;
    }

    public void IncrementQuantityOfApple()
    {
        ++QuantityApple;
    }

    public void IncrementOfScore()
    {
        ++Score;
    }
    public void IncrementOfLevel()
    {
        ++Level;
    }
    public void SetQuantityOfApples(int quantityOfApples)
    {
        QuantityApple = quantityOfApples;
    }
    public void SetMaxLevel(int level)
    {
        if (MaxLevel < level)
            MaxLevel = level;
    }
    public void SetMaxScore(int score)
    {
        if (MaxScore < score)
            MaxScore = score;
    }

    public void ResetLevelValue()
    {
        Level = 0;
    }

    public void ResetValueScore()
    {
        Score = 0;
    }
}
