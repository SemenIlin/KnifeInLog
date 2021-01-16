using UnityEngine;
using UnityEngine.UI;
public class TextController : MonoBehaviour
{
    [SerializeField] private Text _quantityAppleGameWindow;
    [SerializeField] private Text _scoreGameWindow;
    [SerializeField] private Text _levelGameWindow;

    [SerializeField] private Text _quantityAppleResultWindow;
    [SerializeField] private Text _scoreResultWindow;

    [SerializeField] private Text _quantityAppleMenuWindow;
    [SerializeField] private Text _scoreMenuWindow;
    [SerializeField] private Text _levelMenuWindow;

    private GameValues _gameValues;

    public static TextController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _gameValues = GameValues.Instance;
        WoodManager.UpdateCanvas += ShowScore;
        WoodManager.UpdateCanvas += ShowQuantityApple;
    }
    public void ShowQuantityApple()
    {
        _quantityAppleGameWindow.text = _gameValues.QuantityApple.ToString();
        _quantityAppleResultWindow.text = _gameValues.QuantityApple.ToString();
        _quantityAppleMenuWindow.text = _gameValues.QuantityApple.ToString();
    }

    public void ShowScore()
    {
        _scoreGameWindow.text = _gameValues.Score.ToString();
        _scoreResultWindow.text = _gameValues.Score.ToString();
    }

    public void ShowLevel()
    {
        _levelGameWindow.text = (_gameValues.Level + 1).ToString();
    }

    public void ShowMaxScore()
    {
        _scoreMenuWindow.text = _gameValues.MaxScore.ToString();
    }

    public void ShowMaxLevel() 
    {
        _levelMenuWindow.text = (_gameValues.MaxLevel + 1).ToString();
    }
}
