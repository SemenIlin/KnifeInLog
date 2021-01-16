using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _knifeToWood;
    [SerializeField] private AudioClip _knifeToKnife; 
    [SerializeField] private AudioClip _woodCrackling;
    [SerializeField] private AudioClip _appleCut;

    private AudioSource _audioSource;

    public static SoundEffects Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ShootWood()
    { 
        _audioSource.PlayOneShot(_knifeToWood);
    }

    public void ShootKnife()
    {
        _audioSource.PlayOneShot(_knifeToKnife); 
    }

    public void WoodCrackling()
    {
        _audioSource.PlayOneShot(_woodCrackling);
    }

    public void AppleCut()
    {
        _audioSource.PlayOneShot(_appleCut);
    }
}
