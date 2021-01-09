using System;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    private Settings _settings;
    private GameObject _knifeGameObject;
    private float _timer;

    public float force;
    public GameObject knifePrefab;

    // Change visual knife icon.
    public static event Action Shoot;
    public static bool IsShot { get; set; } = true;
    private void Start()
    {
        _settings = LevelController.Instance.GetLevel(GameManager.Level);
        _knifeGameObject = Instantiate(knifePrefab, transform); 
        _timer = _settings.TimerForCreateKnife;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && _timer >= _settings.TimerForCreateKnife)
        {
            if (_knifeGameObject == null)
                return;

            _knifeGameObject.transform.parent = null;
            _knifeGameObject.GetComponent<Knife>().RigidbodyKnife.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            _knifeGameObject = null;
            _timer = 0;
            // Change knife icon.
            Shoot?.Invoke();
        }


        if (WoodManager.IsStartToknifeThrower &&
            _timer >= _settings.TimerForCreateKnife && 
            _knifeGameObject == null &&
            IsShot)
        {
            CreateKnife();
            IsShot = false;
        }
    }

    private void CreateKnife() => _knifeGameObject = Instantiate(knifePrefab, transform);
}
