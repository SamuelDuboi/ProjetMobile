using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private SoundList soundList;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    public void ApplyAudioClip(string name, AudioSource audioSource)
    {
        bool isSelected = false;
        foreach (SoundClassic soundClassic in soundList.soundClassic)
        {
            if (soundClassic.name == name)
            {
                audioSource.clip = soundClassic.clip;
                audioSource.volume = soundClassic.volume;
                isSelected = true;
            }
        }
        if (!isSelected)
        {
            Debug.LogError("No sound have this name");
        }
    }

}
