using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioListener AudioListener;
    public List<AudioClip> Sounds;


    public void Play(string zSoundName)
    {
        if (AudioSource.isPlaying)
        {
            Stop();
        }

        AudioClip clip = Sounds.FirstOrDefault(s => s.name == zSoundName);
        if (clip == null)
        {
            Debug.LogError("Error: sound " + zSoundName + "not recognized");
            return;
        }

        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void Stop()
    {
        AudioSource.Stop();
    }

}
