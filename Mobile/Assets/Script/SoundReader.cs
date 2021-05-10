using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundReader : MonoBehaviour
{
    public string clipName;
    private AudioSource source;
    public bool applyAudioOnStart;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        SoundManager.instance.ApplyAudioClip(name, source);
        if (applyAudioOnStart)
            source.Play();
    }

    public void Play()
    {
        SoundManager.instance.ApplyAudioClip(name, source);
        source.Play();
    }
}
