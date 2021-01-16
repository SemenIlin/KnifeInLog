using System.Collections.Generic;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private List<Window> _windows;

    private void Start()
    {
        TextController.Instance.ShowMaxLevel();
        TextController.Instance.ShowMaxScore();
        TextController.Instance.ShowQuantityApple();

        foreach (var window in _windows)
            window.gameObject.SetActive(window.WindowType == WindowType.Menu);
    }

    public List<Window> Windows => _windows;
}
