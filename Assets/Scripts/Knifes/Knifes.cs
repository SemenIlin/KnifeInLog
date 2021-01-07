using System.Collections.Generic;
using UnityEngine;

public class Knifes : MonoBehaviour
{
    private List<Knife> _knifes;

    public static Knifes Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {        
        _knifes = new List<Knife>();
    }

    public int CountKnifesInWood { get { return _knifes.Count; } }

    public void AddKnife(Knife knife) => _knifes.Add(knife);   

    public void DeleteAllKnife() => _knifes.Clear();

    public void GetNewParent()
    {
        foreach (var knife in _knifes)
        {
            knife.Transform.parent = null;
        }
    }

    public void DisactivateCollider()
    {
        foreach (var knife in _knifes)
        {
            knife.BoxCollider.enabled = false;
        }
    }

    public void AddGravity()
    {
        foreach (var knife in _knifes)
        {
            knife.RigidbodyKnife.isKinematic = false;
            knife.RigidbodyKnife.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)), ForceMode2D.Impulse);
            knife.Transform.Rotate(0f, 0f, Random.Range(-5f, 5f));
            knife.RigidbodyKnife.gravityScale = 3;
        }     
    }
}