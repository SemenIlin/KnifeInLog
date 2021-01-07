using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourcesKnife;
    [SerializeField] private AudioSource _audioSourcesWood;
    [SerializeField] private AudioSource _audioSourcesWoodCrackling;

    private AudioClip _knifeToWood;
    private AudioClip _knifeToKnife; 
    private AudioClip _woodCrackling;

    public static SoundEffects Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        _knifeToKnife = _audioSourcesKnife.clip;
        _knifeToWood = _audioSourcesWood.clip;
        _woodCrackling = _audioSourcesWoodCrackling.clip;
    }

    public void ShootWood() => _audioSourcesWood.PlayOneShot(_knifeToWood);

    public void ShootKnife() => _audioSourcesKnife.PlayOneShot(_knifeToKnife);

    public void WoodCrackling() => _audioSourcesWoodCrackling.PlayOneShot(_woodCrackling);
}
