using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class Knife : MonoBehaviour
{
    [SerializeField] private float _rotate;
    private bool _isFail = false;

    private CircleCollider2D _woodCollider2D;

    private GameValues _gameValues;

    public bool IsKnifeInWood { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    public Rigidbody2D RigidbodyKnife { get; private set; }
    public Transform Transform { get; private set; }

    public static event Action HitOnWood;

    public static event Action HideWood;

    
    private void OnEnable()
    {
        _gameValues = GameValues.Instance;

        RigidbodyKnife = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        Transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -1000)
            Destroy(this.gameObject);

        if (_isFail)
           transform.Rotate(0f, 0f, _rotate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var col = collision.gameObject;
        if (col.tag == "Wood")
        {
            IsKnifeInWood = true;

            if (_woodCollider2D == null)
                _woodCollider2D = col.GetComponent<CircleCollider2D>();

            Knifes.Instance.AddKnife(this);

            // Change position wood, light effect, fail parts.
            HitOnWood?.Invoke();

            _gameValues.IncrementOfScore();
            _gameValues.SetMaxScore(_gameValues.Score);

            SaveManager.Instance.SaveGame();
            TextController.Instance.ShowScore();

            // Stick in knife.
            RigidbodyKnife.velocity = Vector2.zero;
            RigidbodyKnife.isKinematic = true;

            // New parent, allows to rotate knife with wood.
            transform.parent = collision.transform;
            transform.position = NewPositionKnife(col.transform);

            KnifeThrower.IsStartToknifeThrower = true;

            SoundEffects.Instance.ShootWood();
            Vibration.Vibrate(100);
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = collision.gameObject;
        // If shoot to knife in wood or default knife and current knife don't in wood.  
        if ((col.tag == "Knife" && col.GetComponent<Knife>().IsKnifeInWood ||
            col.tag == "KnifeInWood") && !IsKnifeInWood)
        {
            Vibration.VibratePeek();
            SoundEffects.Instance.ShootKnife();
            _gameValues.SetStatusGame(false);
            _isFail = true;

            // Rotate and fail knife after lose shoot.
            RigidbodyKnife.velocity = Vector2.down;
            RigidbodyKnife.gravityScale = 3;

            _gameValues.ResetLevelValue();
            _gameValues.ResetValueScore();

            StartCoroutine(ShowResultWindow());
        }
    }
    private Vector3 NewPositionKnife(Transform parrentTransform)
    {
        var newPosition = parrentTransform.position;
        newPosition.y -= _woodCollider2D.radius + 0.1f;
        return newPosition;
    }

    private IEnumerator ShowResultWindow() 
    {
        yield return new WaitForSeconds(1f);
        HideWood?.Invoke();
       ButtonManager.Instance.ShowWindow((int)WindowType.Restart); 
    }
}
