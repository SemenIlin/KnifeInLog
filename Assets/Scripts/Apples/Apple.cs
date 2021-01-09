using UnityEngine;
[RequireComponent(typeof(DivisionIntoPartsEffect))]
[RequireComponent(typeof(Rigidbody2D))]
public class Apple : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private Rigidbody2D _rigidbody;
    private DivisionIntoPartsEffect _divisionIntoPartsEffect;
    private void Start()
    {
        var _changeCreateApple = Random.Range(0, 101);
        Debug.Log(_changeCreateApple);
        if (_settings.ChangeCreateApple < _changeCreateApple)
            gameObject.SetActive(false);

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        _rigidbody.isKinematic = true;
        WoodManager.FailApple += DeleteParent;
        _divisionIntoPartsEffect = GetComponent<DivisionIntoPartsEffect>();
        _divisionIntoPartsEffect.DisableParts();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Knife")
        {
            ++GameManager.QuantityApple;
            SaveManager.Instance.SaveGame();
            TextController.Instance.ShowQuantityApple();
            SoundEffects.Instance.AppleCut();

            // Division aplle on parts.
            _divisionIntoPartsEffect.GetNewParent();
            _divisionIntoPartsEffect.EnableSpriteRendererForParts();
            _divisionIntoPartsEffect.AddForceGravicyParts();
            Destroy(gameObject);
        }
    }

    private void DeleteParent()
    {
        if (gameObject != null)
        {
            gameObject.transform.parent = null;
            _rigidbody.isKinematic = false;
            _rigidbody.gravityScale = 4;
        }
    }

    private void OnDestroy()
    {
        WoodManager.FailApple -= DeleteParent;
    }
}
