using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlinkEffects : MonoBehaviour
{
    [SerializeField] private Material _woodMaterialBlink;
    private Material _woodMaterial;

    private Vector3 _oldPosition;
    private Vector3 _newPosition;
    [SerializeField] private float offset;

    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _oldPosition = transform.position;
        _newPosition = new Vector3(_oldPosition.x, _oldPosition.y + offset, _oldPosition.z);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _woodMaterial = _spriteRenderer.material;

        Knife.HitOnWood += ShowHit;
    }    

    public void ShowHit()
    {
        transform.position = _newPosition;
        _spriteRenderer.material = _woodMaterialBlink;        
        Invoke("ResetAll", 0.1f);
    }
    
    public void ResetAll()
    {
        ResetMaterial();
        ResetPosition();
    }
    public void ResetMaterial() => _spriteRenderer.material = _woodMaterial;
    public void ResetPosition() => transform.position = _oldPosition;

    private void OnDestroy()
    {
        Knife.HitOnWood -= ShowHit;
    }
}
