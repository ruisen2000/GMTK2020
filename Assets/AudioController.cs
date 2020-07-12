using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioController instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //prevents duplicate AudioControllers
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    public void Play( string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            //if sound is not found, no sound plays
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
       s.source.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("Theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
