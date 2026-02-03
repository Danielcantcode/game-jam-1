using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // LOAD: When the menu starts, look for saved values. 
        // If none exist, use a default of 0.75f.
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // Apply these to the UI handles
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;

        // Apply these to the actual Mixer
        SetMusicVolume(savedMusic);
        SetSFXVolume(savedSFX);
    }

    public void SetMusicVolume(float value)
    {
        mainMixer.SetFloat("MusicVol", Mathf.Log10(value) * 20);
        
        // SAVE: Store the value as "MusicVolume"
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        mainMixer.SetFloat("SFXVol", Mathf.Log10(value) * 20);
        
        // SAVE: Store the value as "SFXVolume"
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    // Call this when closing the menu to be 100% sure it's written to disk
    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }
}