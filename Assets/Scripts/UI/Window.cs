using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private WindowType _windowType;

    public WindowType WindowType => _windowType;
}
