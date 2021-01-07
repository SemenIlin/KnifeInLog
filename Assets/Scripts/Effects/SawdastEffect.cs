using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawdastEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _sawdust;
    private void Start()
    {
        Knife.HitOnWood += ShowSawdust;
    }

    private void ShowSawdust()
    {
        _sawdust.Play();
    }
}
