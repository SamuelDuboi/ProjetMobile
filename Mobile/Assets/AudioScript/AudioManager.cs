using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    //l'audiomanager peut être appelé de n'importe où avec "AudioManager.instance.MaFonction();"
    public AudioMixerGroup mixer;
    public AudioMixer mix;
    public int soundArrayNumber; //à remplir quand on ajoute un tableau de Sound
    public int stepArrayNumber;  //Same
    Sound[][] soundArrays;
    Sound[][] stepArray;
    public Sound[] son;

    public Sound[] pasHerbe; //index 0

    public Sound[] jump; //index 0
    public Sound[] effort; //index 1
    public Sound[] arriveHauteur; //index 2
    public Sound[] chute; //index 3

    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        soundArrays = new Sound[soundArrayNumber][];
        soundArrays[0] = jump;//mettre les arrays suivants comme ça
        soundArrays[1] = effort;
        soundArrays[2] = arriveHauteur;
        soundArrays[3] = chute;

        stepArray = new Sound[stepArrayNumber][];
        //stepArray[0] = pasHerbe;

        CreateStepSource();

    }

    //Joue un son unique qui n'est pas dans une liste de son
    public void PlayUnique(int index)   //l'index correspond à l'index du tableau "son"
    {
        Sound s = son[index];

        s.source = gameObject.AddComponent<AudioSource>();
        s.source.outputAudioMixerGroup = mixer;
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;

        s.source.Play();
        StartCoroutine(DestroySource(s.source));
    }

    //Joue un son random dans une liste de son
    public void PlayArray(int index)    //l'index correspond au tableau de son que le joueur veut jouer
    {
        Sound[] sArray = soundArrays[index];
        Sound s = sArray[Random.Range(0, sArray.Length - 1)];

        s.source = gameObject.AddComponent<AudioSource>();
        s.source.outputAudioMixerGroup = mixer;
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;

        s.source.Play();
        StartCoroutine(DestroySource(s.source));
    }

    //Joue un sonde pas random dans une liste de son de pas
    public void PlayStep(int index)     //l'index correspond au tableau de son de pas que le joueur veut jouer
    {
        Sound[] sArray = stepArray[index];
        Sound s = sArray[Random.Range(0, sArray.Length - 1)];

        s.source.Play();
    }

    //Detruit une audioSource quand le clip a fini de se jouer
    IEnumerator DestroySource(AudioSource source)   
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(source);
    }

    //crée les audioSources pour les bruits de pas
    void CreateStepSource()
    {
        for (int i = 0; i < stepArrayNumber; i++)
        {
            for (int y = 0; y < stepArray[i].Length; y++)
            {
                Sound s = stepArray[i][y];
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.outputAudioMixerGroup = mixer;
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
    }

}
