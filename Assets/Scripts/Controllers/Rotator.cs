using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    [Range(-20f, 20f)]
    private float _rotateX;

    [SerializeField]
    [Range(-20f, 20f)]
    private float _rotateY;
    private void FixedUpdate()
    {
        transform.Rotate(_rotateX, _rotateY, 0);
    }
}
