using System;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    private Settings _settings;
    private GameObject _knifeGameObject;
    private float _timer;

    public float force;
    public GameObject knifePrefab;

    public static bool IsCreateKnife = false;

    // Change visual knife icon.
    public static event Action Shoot;

    /// <summary>
    ///   Spawn new knife when old knife in wood.
    /// </summary>
    public static bool IsStartToknifeThrower { get; set; } = false;
    private void Start()
    {
        ButtonManager.Instance.CreateEnviromentLevel += CreateKnife;
        WoodManager.CreateKnife += CreateKnife;
        _settings = LevelController.Instance.GetLevel(GameManager.Level);
        _timer = _settings.TimerForCreateKnife;
    }

    private void Update()
    {
        if (GameManager.IsGame)
        {
            _timer += Time.deltaTime;

            if (IsCreateKnife &&
                _knifeGameObject != null)
            {
                _knifeGameObject.GetComponent<Knife>().RigidbodyKnife.AddForce(Vector2.up * force, ForceMode2D.Impulse);

                _knifeGameObject.transform.parent = null;
                _knifeGameObject = null;
                _timer = 0;
                // Change knife icon.
                Shoot?.Invoke();
                IsCreateKnife = false;
            }


            if (_timer >= _settings.TimerForCreateKnife &&
                IsStartToknifeThrower &&
                WoodManager.IsCreatedWood &&
                _knifeGameObject == null)
            {
                CreateKnife(); 
                IsStartToknifeThrower = false;
            }
        }    
    }

    private void CreateKnife() => _knifeGameObject = Instantiate(knifePrefab, transform);
}
