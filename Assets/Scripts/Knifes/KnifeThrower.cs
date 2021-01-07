using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    private GameObject _knifeGameObject;

    public float force;
    public GameObject knifePrefab;
    private void Start()
    {
        _knifeGameObject = Instantiate(knifePrefab, transform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _knifeGameObject.transform.parent = null;
            _knifeGameObject.GetComponent<Knife>().RigidbodyKnife.AddForce(Vector2.up * force, ForceMode2D.Impulse);

            _knifeGameObject = Instantiate(knifePrefab, transform);
        }
    }
}
