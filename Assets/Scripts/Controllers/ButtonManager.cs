using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private WindowsController _windowsController;
	
    public static ButtonManager Instance;

    public event Action CreateEnviromentLevel;
    public event Action DestroyWood;
    public event Action ShoowWood;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowWindow(int windowType)
    {
        foreach (var window in _windowsController.Windows)
        {
            window.gameObject.SetActive(window.WindowType == (WindowType)windowType);
        }
    }

    public void CreateKnife()
    {
        KnifeThrower.IsCreateKnife = true;
    }
    public void StartGame()
    {
        GameValues.Instance.SetStatusGame(true);
        CreateEnviromentLevel?.Invoke();
    }

    public void RestartGame()
    {
        GameValues.Instance.SetStatusGame(true);
        DestroyWood?.Invoke();
        ShoowWood?.Invoke();
        CreateEnviromentLevel?.Invoke();
    }

    public void BackHome()
    {
        DestroyWood?.Invoke();
        TextController.Instance.ShowMaxLevel();
        TextController.Instance.ShowMaxScore();
    }
}
