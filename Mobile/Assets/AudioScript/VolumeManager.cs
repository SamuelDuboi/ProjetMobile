using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField][Range(1, 20)]
    private float MaxVolume;

    [SerializeField][Range(-10, 0)]
    private float MinVolume;

    [SerializeField]
    private Slider MasterVolumeSlider;
    private float oldMasterVolume = 0;
    [SerializeField]
    private Slider SfxVolumeSlider;
    private float oldSfxVolume = 0;
    [SerializeField]
    private Slider MusicVolumeSlider;
    private float oldMusicVolume = 0;

    [SerializeField]
    private AudioMixer Mixer;

    private string masterVolumeSave = "1";
    private string sfxVolumeSave = "2";
    private string musicVolumeSave = "3";


    private void Start()
    {
        MasterVolumeSlider.maxValue = MaxVolume;
        SfxVolumeSlider.maxValue = MaxVolume;
        MusicVolumeSlider.maxValue = MaxVolume;
        MasterVolumeSlider.minValue = MinVolume;
        SfxVolumeSlider.minValue = MinVolume;
        MusicVolumeSlider.minValue = MinVolume;




        if (PlayerPrefs.HasKey(masterVolumeSave))
            oldMasterVolume = PlayerPrefs.GetFloat(masterVolumeSave);
        if (PlayerPrefs.HasKey(sfxVolumeSave))
            oldSfxVolume = PlayerPrefs.GetFloat(sfxVolumeSave);
        if (PlayerPrefs.HasKey(musicVolumeSave))
            oldMusicVolume = PlayerPrefs.GetFloat(musicVolumeSave);

        MasterVolumeSlider.value = oldMasterVolume;
        Mixer.SetFloat("MasterVolume", Mathf.Log(oldMasterVolume, 1.3f));

        SfxVolumeSlider.value = oldSfxVolume;
        Mixer.SetFloat("SfxVolume", Mathf.Log(oldSfxVolume, 1.3f));

        MusicVolumeSlider.value = oldMusicVolume;
        Mixer.SetFloat("MusicVolume", Mathf.Log(oldMusicVolume, 1.3f));
    }


    public void ChangeMasterVolume()
    {
        oldMasterVolume = MasterVolumeSlider.value;       
        Mixer.SetFloat("MasterVolume", Mathf.Log(oldMasterVolume, 1.3f));
        PlayerPrefs.SetFloat(masterVolumeSave, oldMasterVolume);
    }
    public void ChangeSfxVolume()
    {
        oldSfxVolume = SfxVolumeSlider.value;
        Mixer.SetFloat("SfxVolume", Mathf.Log(oldSfxVolume, 1.3f));
        PlayerPrefs.SetFloat(sfxVolumeSave, oldSfxVolume);
    }
    public void ChangeMusicVolume()
    {
        oldMusicVolume = MusicVolumeSlider.value;
        Mixer.SetFloat("MusicVolume", Mathf.Log(oldMusicVolume, 1.3f));
        PlayerPrefs.SetFloat(musicVolumeSave, oldMusicVolume);
    }
}
