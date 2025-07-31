using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSliderMenu : MonoBehaviour
{
    public AudioSource audioPlayer;
    public AudioClip soundEffectClip;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider master;
    [SerializeField] private Slider soundEffect;
    [SerializeField] private Slider music;

    private string masterVolume = "MasterVolume";
    private string musicVolume = "MusicVolume";
    private string soundEffectVolume = "SoundEffectVolume";

    /// <summary>
    /// When the Slider for Master Volume is moved, change both the volume and the Number "100" to the according value of the Slider
    /// </summary>
    public void ChangeMaster(float volume)
    {
        audioMixer.SetFloat(masterVolume, Mathf.Log10(volume) * 20f);
    }

    /// <summary>
    /// When the Slider for Music Volume is moved, change both the volume and the Number "100" to the according value of the Slider
    /// </summary>
    public void ChangeMusic(float volume)
    {
        audioMixer.SetFloat(musicVolume, Mathf.Log10(volume) * 20f);
    }

    /// <summary>
    /// When the Slider for SoundEffects Volume is moved, change both the volume and the Number "100" to the according value of the Slider
    /// </summary>
    public void ChangeSoundEffect(float volume)
    {
        audioMixer.SetFloat(soundEffectVolume, Mathf.Log10(volume) * 20f);
    }

    /// <summary>
    /// On Pressing "Check Sound" button, play SoundEffect
    /// </summary>
    public void CheckSoundEffect()
    {
        audioPlayer.PlayOneShot(soundEffectClip);
    }
}
