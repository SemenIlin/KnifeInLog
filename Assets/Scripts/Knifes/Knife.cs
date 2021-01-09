using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class Knife : MonoBehaviour
{
    [SerializeField] private float _rotate;
    private bool _isFail = false;

    private CircleCollider2D _woodCollider2D;

    public BoxCollider2D BoxCollider { get; private set; }
    public Rigidbody2D RigidbodyKnife { get; private set; }
    public Transform Transform { get; private set; }
    public bool IsAttacedToWood { get; private set; } = false;
    public static event Action HitOnWood;
    public static event Action ChangeWood; 

    private void Start()
    {
        Vibration.Init();
        RigidbodyKnife = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        Transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (_isFail && !IsAttacedToWood)
            transform.Rotate(0f, 0f, _rotate);

        if (transform.position.y < -10)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var col = collision.gameObject;
        if (col.tag == "Wood")
        {
            if (_woodCollider2D == null)
                _woodCollider2D = col.GetComponent<CircleCollider2D>();

            Knifes.Instance.AddKnife(this);
            ++GameManager.Score;
            SaveManager.Instance.SaveGame();
            TextController.Instance.ShowScore();

            ChangeWood?.Invoke();
            // Stick in knife.
            RigidbodyKnife.velocity = Vector2.zero;
            RigidbodyKnife.isKinematic = true;

            // New parent, allows to rotate knife with wood.
            transform.parent = collision.transform;
            transform.position = NewPositionKnife(col.transform);
            IsAttacedToWood = true;
            
            // Change position wood, light effect, fail parts.
            HitOnWood?.Invoke();

            // spawn new knife when old knife in wood.
            KnifeThrower.IsShot = true;

            Vibration.VibratePeek();
            SoundEffects.Instance.ShootWood();
        }

        if (col.tag == "Knife" && col.GetComponent<Knife>().IsAttacedToWood ||
            col.tag == "KnifeInWood")
        {
            Vibration.VibratePop();
            SoundEffects.Instance.ShootKnife();
            _isFail = true;
            GameManager.IsGame = false;

            // do not re-tap knife to wood or knife.
            BoxCollider.enabled = false;

            // Rotate and fail knife after lose shoot.
            RigidbodyKnife.velocity = Vector2.one;
            RigidbodyKnife.gravityScale = 3;            
        }
    }

    private Vector3 NewPositionKnife(Transform parrentTransform)
    {
        var newPosition = parrentTransform.position;
        newPosition.y -= _woodCollider2D.radius;
        return newPosition;
    }
}
