using UnityEngine;

public class BossEffect : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime = 2.5f;
    private bool _isLife = false;

    private void OnEnable()
    {
        _isLife = true;
    }
    private void Update()
    {
        if (transform.parent == null)
        {
            if (_isLife)
                _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0)
                Destroy(gameObject);
        }
    }
}
