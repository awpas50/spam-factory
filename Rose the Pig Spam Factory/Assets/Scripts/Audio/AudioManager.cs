using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // ref:https://www.youtube.com/watch?v=6OT43pvUyfY
    
    public Sound[] sounds;
    public static AudioManager instance;
    public static Dictionary<SoundList, float> soundTimerDict;
    public static Dictionary<SoundList, bool> soundConditionDict;

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

        soundConditionDict = new Dictionary<SoundList, bool>();
        soundTimerDict = new Dictionary<SoundList, float>();

        foreach (SoundList val in Enum.GetValues(typeof(SoundList)))
        {
            soundConditionDict[val] = false;
            soundTimerDict[val] = 0;
        }
    }

    public static bool CanPlaySound(SoundList soundList, float interval)
    {
        Debug.Log(Time.time + interval + " " + soundTimerDict[soundList]);
        float lastTimePlayed = soundTimerDict[soundList];
        if (lastTimePlayed + interval < Time.time)
        {
            soundTimerDict[soundList] = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Use this to play audio once in trigger function (e.g. OnTriggerEnter, OnMouseButtonUp, OnMouseButtonDown)
    public void Play(SoundList soundList)
    {
        // Create an empty game object, than add a audioSource & audioClip on it.
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        AudioClip audioClip = GetAudioClip(soundList);

        audioSource.volume = GetVolume(soundList);
        audioSource.pitch = GetPitch(soundList);
        audioSource.reverbZoneMix = GetReverbZoneMix(soundList);
        // Play the audio clip
        audioSource.PlayOneShot(audioClip);
        
        Destroy(soundGameObject, 10f);
    }

    // Play only once in Update() to avoid playing the clip every frame.
    public void PlayOnce(SoundList soundList)
    {
        if(soundConditionDict[soundList] == false)
        {
            // Create an empty game object, than add a audioSource & audioClip on it.
            GameObject soundGameObject = new GameObject("Sound in Update()");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            AudioClip audioClip = GetAudioClip(soundList);

            audioSource.volume = GetVolume(soundList);
            audioSource.pitch = GetPitch(soundList);
            audioSource.reverbZoneMix = GetReverbZoneMix(soundList);
            // Play the audio clip
            audioSource.PlayOneShot(audioClip);

            // the dictionary value is set to true to avoid playing the clip every frame.
            // set it to false using "soundConditionDict[soundList] = false;" in any other part of the script.
            soundConditionDict[soundList] = true;
            Destroy(soundGameObject, 10f);
        }
    }

    public void PlayContinuously(SoundList soundList, float interval)
    {
        if (CanPlaySound(soundList, interval))
        {
            // Create an empty game object, than add a audioSource & audioClip on it.
            GameObject soundGameObject = new GameObject("Sound continuous");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            AudioClip audioClip = GetAudioClip(soundList);

            audioSource.volume = GetVolume(soundList);
            audioSource.pitch = GetPitch(soundList);
            audioSource.reverbZoneMix = GetReverbZoneMix(soundList);
            // Play the audio clip
            audioSource.PlayOneShot(audioClip);

            Destroy(soundGameObject, 10f);
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
