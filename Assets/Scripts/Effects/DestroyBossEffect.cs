using UnityEngine;

public class DestroyBossEffect : MonoBehaviour, IDestroy
{
    [SerializeField] private ParticleSystem _destroyEffect;

    public void DestroyTarget()
    {
        _destroyEffect.transform.parent = null;
        _destroyEffect.Play();
    }
}
