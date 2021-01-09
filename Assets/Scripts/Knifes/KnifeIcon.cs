using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KnifeIcon : MonoBehaviour
{
    [SerializeField] private Sprite _usedKnifeSprite;
    private Sprite _defaultSprite;

    private Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
        _defaultSprite = _image.sprite;
    }

    public void SetDefaultSprite() => _image.sprite = _defaultSprite;
    public void ChangeIconKnife() => _image.sprite = _usedKnifeSprite;
}
