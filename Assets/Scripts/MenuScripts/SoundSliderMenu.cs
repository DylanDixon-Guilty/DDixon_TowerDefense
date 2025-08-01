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
    private float volumeMultiplier = 20f; // This goes in the ChangeMaster/Music/Sound Functions

    private void Awake()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMasterVolume();
        }
        else
        {
            ChangeMaster();
        }
    }

    /// <summary>
    /// When the Slider for Master Volume is moved, change the volume to the according value of the Slider
    /// </summary>
    public void ChangeMaster()
    {
        float volume = master.value;
        audioMixer.SetFloat(masterVolume, Mathf.Log10(volume) * volumeMultiplier);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadMasterVolume()
    {
        master.value = PlayerPrefs.GetFloat("musicVolume");
        ChangeMaster();
    }

    /// <summary>
    /// When the Slider for Music Volume is moved, change the volume to the according value of the Slider
    /// </summary>
    public void ChangeMusic()
    {
        float volume = master.value;
        audioMixer.SetFloat(musicVolume, Mathf.Log10(volume) * volumeMultiplier);
        PlayerPrefs.SetFloat(musicVolume, volume);
    }

    private void LoadMusicVolume()
    {
        music.value = PlayerPrefs.GetFloat(musicVolume);
        ChangeMusic();
    }

    /// <summary>
    /// When the Slider for SoundEffects Volume is moved, change the volume to the according value of the Slider
    /// </summary>
    public void ChangeSoundEffect()
    {
        float volume = master.value;
        audioMixer.SetFloat(soundEffectVolume, Mathf.Log10(volume) * volumeMultiplier);
    }

    /// <summary>
    /// On Pressing "Check Sound" button, play SoundEffect
    /// </summary>
    public void CheckSoundEffect()
    {
        audioPlayer.PlayOneShot(soundEffectClip);
    }
}
