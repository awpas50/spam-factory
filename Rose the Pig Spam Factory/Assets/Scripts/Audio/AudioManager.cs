using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // ref:https://www.youtube.com/watch?v=6OT43pvUyfY

    public Sound[] sounds;
    public static AudioManager instance;
    private static Dictionary<SoundList, float> soundTimerDictionary;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        soundTimerDictionary = new Dictionary<SoundList, float>();
        soundTimerDictionary[SoundList.PlayerMove] = 0;
        soundTimerDictionary[SoundList.PlayerMove_2] = 0;
    }

    public static bool CanPlaySound(SoundList sound, float CD)
    {
        if (soundTimerDictionary.ContainsKey(sound))
        {
            float lastTimePlayed = soundTimerDictionary[sound];
            if (lastTimePlayed + CD < Time.time)
            {
                soundTimerDictionary[sound] = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public void Play(SoundList soundList, float CD)
    {
        if(CanPlaySound(soundList, CD))
        {
            // Create an empty game object, than add a audioSource & audioClip on it.
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            AudioClip audioClip = GetAudioClip(soundList);

            audioSource.volume = GetVolume(soundList);
            audioSource.pitch = GetPitch(soundList);
            audioSource.reverbZoneMix = GetReverbZoneMix(soundList);
            audioSource.PlayOneShot(audioClip);

            Destroy(soundGameObject, 5f);
        }
    }

    public AudioClip GetAudioClip(SoundList soundList)
    {
        // Access particular clip according to the enum variable.
        foreach(Sound s in sounds)
        {
            if(s.soundList == soundList)
            {
                return s.clip;
            }
        }
        Debug.LogError("Sound " + soundList + " not found!");
        return null;
    }

    public float GetVolume(SoundList soundList)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundList == soundList)
            {
                return s.volume;
            }
        }
        return 0;
    }

    public float GetPitch(SoundList soundList)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundList == soundList)
            {
                return s.pitch;
            }
        }
        return 0;
    }

    public float GetReverbZoneMix(SoundList soundList)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundList == soundList)
            {
                return s.reverbZoneMix;
            }
        }
        return 0;
    }
}
