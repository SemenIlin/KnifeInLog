using UnityEngine;
public class DivisionIntoPartsEffect : MonoBehaviour
{
    [SerializeField] private GameObject[] _parts;
    [SerializeField] private Vector2 _force;

    private void Start()
    {
        _force.x = _force.x >= _force.y ? _force.y : _force.x;
        _force.y = _force.y >= _force.x ? _force.y : _force.x;
    }
    public void AddForceGravicyParts()
    {
        foreach (var part in _parts)
        {
            var tempPart = part.GetComponent<Rigidbody2D>();
            tempPart.AddForce(new Vector2(Random.Range(_force.x, _force.y), Random.Range(_force.x, _force.y)), ForceMode2D.Impulse);
            tempPart.gravityScale = 4;
        }
    }
    public void GetNewParent()
    {
        foreach (var part in _parts)
        {
            part.GetComponent<Transform>().parent = null;
        }
    }
    public void EnableSpriteRendererForParts()
    {
        foreach (var part in _parts)
        {
            part.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void DisableParts()
    {
        foreach (var part in _parts)
        {
            var tempPart = part.GetComponent<Rigidbody2D>();
            tempPart.gravityScale = 0;
            part.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
