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

    public static TextController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        WoodManager.UpdateCanvas += ShowScore;
        WoodManager.UpdateCanvas += ShowQuantityApple;
    }
    public void ShowQuantityApple()
    {
        _quantityAppleGameWindow.text = GameManager.QuantityApple.ToString();
        _quantityAppleResultWindow.text = GameManager.QuantityApple.ToString();
        _quantityAppleMenuWindow.text = GameManager.QuantityApple.ToString();
    }

    public void ShowScore()
    {
        _scoreGameWindow.text = GameManager.Score.ToString();
        _scoreResultWindow.text = GameManager.Score.ToString();
    }

    public void ShowLevel()
    {
        _levelGameWindow.text = (GameManager.Level + 1).ToString();
    }

    public void ShowMaxScore()
    {
        _scoreMenuWindow.text = GameManager.MaxScore.ToString();
    }

    public void ShowMaxLevel() 
    {
        _levelMenuWindow.text = (GameManager.MaxLevel + 1).ToString();
    }
}
