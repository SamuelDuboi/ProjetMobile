using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New SoundList", menuName = "SoundList", order = 50)]
[System.Serializable]
public class SoundList : ScriptableObject
{
    [SerializeField] public List<SoundClassic> soundClassic = new List<SoundClassic>();
    [SerializeField] public List<Music> music = new List<Music>();
}
[System.Serializable]
public class Music
{
    public string name = string.Empty;
    public AudioClip clip = default;
}
[System.Serializable]
public class SoundClassic : Music
{
    public float volume = 0.5f;
}

