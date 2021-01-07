using UnityEngine;
using System;

public class Knife : MonoBehaviour
{
    [SerializeField] private float _rotate;
    private bool _isFail = false;

    public BoxCollider2D BoxCollider { get; private set; }
    public Rigidbody2D RigidbodyKnife { get; private set; }
    public Transform Transform { get; private set; }
    public bool IsAttacedToWood { get; private set; } = false;
    public static event Action HitOnWood;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wood")
        {
            Knifes.Instance.AddKnife(this);
            // Stick in knife.
            RigidbodyKnife.velocity = Vector2.zero;
            RigidbodyKnife.isKinematic = true;

            // New parent, allows to rotate knife with wood.
            transform.parent = collision.transform;            
            IsAttacedToWood = true;
            
            // Change position wood, light effect, fail parts.
            HitOnWood?.Invoke();

            Vibration.VibratePeek();
            SoundEffects.Instance.ShootWood();
        }

        if (collision.gameObject.tag == "Knife")
        {
            if (collision.gameObject.GetComponent<Knife>().IsAttacedToWood)
            {
                Vibration.VibratePop();
                SoundEffects.Instance.ShootKnife();
                _isFail = true;

                // do not re-tap knife to wood or knife.
                BoxCollider.enabled = false;

                // Rotate and fail knife after lose shoot.
                RigidbodyKnife.velocity = Vector2.one;
                RigidbodyKnife.gravityScale = 3;
            }
        }
    }
}
