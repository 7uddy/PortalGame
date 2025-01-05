using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start ()
    {
        LoadVolume();
    }

    private void OnDisable()
    {
        SaveVolume();
    }
    public void SetMasterVolumeFromSlider(float level)
    {
        AudioMixer.SetFloat("MasterVolume", level);
        MasterSlider.value = level;
    }
    public void SetMusicVolumeFromSlider(float level)
    {
        AudioMixer.SetFloat("MusicVolume", level);
        MusicSlider.value = level;
    }

    public void SetSFXVolumeFromSlider(float level)
    {
        AudioMixer.SetFloat("SFXVolume", level);
        SFXSlider.value = level;
    }

    public void SaveVolume()
    {
        Debug.Log("SAVING PREFERENCE: SOUND VOLUME");

        AudioMixer.GetFloat("MasterVolume", out float masterVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);

        AudioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        AudioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        SetMasterVolumeFromSlider(masterVolume);

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolumeFromSlider(musicVolume);

        float SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolumeFromSlider(SFXVolume);

        Debug.Log($"LOADING PREFERENCE: MASTER: {masterVolume}, MUSIC: {musicVolume}, FLOAT: {SFXVolume}");
    }
}