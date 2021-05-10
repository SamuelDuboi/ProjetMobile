using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private SoundList soundList;
    private List<AudioSource> sources;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

   private void Start()
    {
        EventManager.instance.ActiveSound += ActiveSound;   
    }
    public void ApplyAudioClip(string name, AudioSource audioSource)
    {
        bool isSelected = false;
        foreach (Music soundClassic in soundList.music)
        {
            if (soundClassic.name == name)
            {
                audioSource.clip = soundClassic.clip;
                isSelected = true;
                if (sources == null)
                    sources = new List<AudioSource>();
                if (!sources.Contains(audioSource))
                    sources.Add(audioSource);
            }
        }
        if (!isSelected)
        {
            Debug.LogError("No sound have this name");
        }
    }

    public void ActiveSound(bool isActive)
    {
        if (isActive)
        {
            foreach (var sound in sources)
            {
                sound.mute = true;
            }
        }
        else
        {
            foreach (var sound in sources)
            {
                sound.mute = false;
            }
        }
    }

    public void ToogleSound()
    {
        EventManager.instance.OnActiveSound();
    }

    public void ClearSound()
    {
        sources.Clear();
    }
}
