using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;
    public static AudioManager InstanceAM { get; private set; }


    private void Awake()
    {
        //SINGLETON PATTERN 
       if(InstanceAM != null)  
        {
            Destroy(this);
        }
       else
        {
            InstanceAM = this;
            DontDestroyOnLoad(this);
        }

    }
    public void PlaySound(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
        
    }
    public void StopSound()
    {
        sfxAudioSource.Stop();
        musicAudioSource.Stop();

    }

    public void PlaySoundEffect(AudioClip soundName)
    {

        sfxAudioSource.PlayOneShot(soundName);

    }




}
