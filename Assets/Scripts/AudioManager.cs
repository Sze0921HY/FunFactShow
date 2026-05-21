using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource_SFX;
    public AudioSource audioSource_BGM;

    public Slider volumeSlider;

    [Header(header: "SFX")]
    public AudioClip CorrectSFX;
    public AudioClip WrongSFX;
    public AudioClip CountDownSFX;


    [Header(header: "Music")]
    public AudioClip BGM;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource_SFX.volume = volumeSlider.value;
        audioSource_BGM.volume = volumeSlider.value;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SetVolume(float volume)
    {
        audioSource_SFX.volume = volume;
        audioSource_BGM.volume = volume;
    }

    public void playCorrectSFX()
    {
        audioSource_SFX.Stop();
        audioSource_SFX.clip = CorrectSFX;
        audioSource_SFX.Play();
    }

    public void playWrongSFX()
    {
        audioSource_SFX.Stop();
        audioSource_SFX.clip = WrongSFX;
        audioSource_SFX.Play();
    }

    public void PlayCountdownSFX()
    {
        audioSource_SFX.Stop();
        audioSource_SFX.clip = CountDownSFX;
        audioSource_SFX.Play();
    }
}
