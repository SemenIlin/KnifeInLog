using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float _rotateX;
    private float _rotateY;
    private void Start()
    {
        _rotateX = Random.Range(-20f, 20f);
        _rotateY = Random.Range(-20f, 20f);
    }
    private void FixedUpdate()
    {
        transform.Rotate(_rotateX, _rotateY, 0);
    }
}
