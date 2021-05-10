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
        SoundManager.instance.ApplyAudioClip(clipName, source);
        if (applyAudioOnStart)
            source.Play();
    }

    public void Play()
    {
        SoundManager.instance.ApplyAudioClip(clipName, source);
        source.Play();
    }
}
