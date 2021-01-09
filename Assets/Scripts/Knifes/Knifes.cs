using System.Collections.Generic;
using UnityEngine;

public class Knifes : MonoBehaviour
{
    private List<Knife> _knifesInWood;

    public static Knifes Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        _knifesInWood = new List<Knife>();
    }

    public int CountKnifesInWood { get { return _knifesInWood.Count; } }

    public void AddKnife(Knife knife) => _knifesInWood.Add(knife);   

    public void DeleteAllKnife() => _knifesInWood.Clear();

    public void GetNewParent()
    {
        foreach (var knife in _knifesInWood)
        {
            knife.Transform.parent = null;
        }
    }

    public void DisactivateCollider()
    {
        foreach (var knife in _knifesInWood)
        {
            knife.BoxCollider.enabled = false;
        }
    }

    public void AddGravity()
    {
        foreach (var knife in _knifesInWood)
        {
            knife.RigidbodyKnife.isKinematic = false;
            knife.RigidbodyKnife.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)), ForceMode2D.Impulse);
            knife.Transform.Rotate(0f, 0f, Random.Range(-15f, 15f));
            knife.RigidbodyKnife.gravityScale = 3;
        }     
    }
}