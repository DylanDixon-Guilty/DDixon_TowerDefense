using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SoundSliderMenu : MonoBehaviour
{
    public AudioSource audioPlayer;
    public AudioClip soundEffectClip;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Slider musicSlider;

    private string masterVolume = "MasterVolume";
    private string musicVolume = "MusicVolume";
    private string soundEffectVolume = "SoundEffectVolume";
    private const string masterVolumeKey = "MasterVolumeKey"; //Key-word to save PlayerPrefs
    private const string musicVolumeKey = "MusicVolumeKey";
    private const string soundEffectVolumeKey = "SoundEffectVolumeKey";
    private float volumeMultiplier = 20f; // This goes in the ChangeMaster/Music/Sound Functions
    private int defaultVolume = 1; //The default of the volume if the player has not set it yet

    private void Start()
    {
        LoadVolume(masterVolumeKey, masterSlider, masterVolume);
        LoadVolume(musicVolumeKey, musicSlider, musicVolume);
        LoadVolume(soundEffectVolumeKey, soundEffectSlider, soundEffectVolume);
    }

    /// <summary>
    /// This changes the volume whenever the slider moved to the left or right
    /// based on the parameters in the Functions(ChangeMaster/Music/SoundEffect) down below.
    /// Then it saves it to a keyword given in said Functions
    /// </summary>
    private void ChangeVolume(string playerPrefsKey, string mixerParameter, float volume)
    {
        audioMixer.SetFloat(mixerParameter, Mathf.Log10(volume) * volumeMultiplier);
        PlayerPrefs.SetFloat(playerPrefsKey, volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// When the player changes any of the sliders they are saved in ChangeVolume.
    /// Then this functions handles setting the values the player set when the player quits and re-enters the game based on that saved data
    /// </summary>
    private void LoadVolume(string playerPrefsKey, Slider slider, string mixerParameter)
    {
        float savedVolume = PlayerPrefs.GetFloat(playerPrefsKey, defaultVolume);
        slider.value = savedVolume;
        audioMixer.SetFloat(mixerParameter, Mathf.Log10(savedVolume) * volumeMultiplier);
    }

    /// <summary>
    /// When the Slider for Master Volume is moved, change the volume to the according value of the Slider
    /// </summary>
    public void ChangeMaster()
    {
        ChangeVolume(masterVolumeKey, masterVolume, masterSlider.value);
    }

    /// <summary>
    /// When the Slider for Music Volume is moved, change the volume to the according value of the Slider
    /// </summary>
    public void ChangeMusic()
    {
        ChangeVolume(musicVolumeKey, musicVolume, musicSlider.value);
    }

    /// <summary>
    /// When the Slider for SoundEffects Volume is moved, change the volume to the according value of the Slider
    /// </summary>
    public void ChangeSoundEffect()
    {
        ChangeVolume(soundEffectVolumeKey, soundEffectVolume, soundEffectSlider.value);
    }

    /// <summary>
    /// On Pressing "Check Sound" button, play SoundEffect
    /// </summary>
    public void CheckSoundEffect()
    {
        audioPlayer.PlayOneShot(soundEffectClip);
    }
}
