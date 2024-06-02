using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioFile[] AudioFiles;

    public AudioSource AudioSource_Music;
    public AudioSource AudioSource_SFX;

    public float OverallVolume_Music;
    public float OverallVolume_SFX;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
        if (AudioSource_Music == null)
        {
            AudioSource_Music = gameObject.AddComponent<AudioSource>();
        }

        if (AudioSource_SFX == null)
        {
            AudioSource_SFX = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlayMusic(string soundName)
    {
        var file = GetFileByName(soundName);

        if (file != null)
        {
            var clip = file.Clip;
            AudioSource_Music.volume = file.Volume * OverallVolume_Music;
            if (clip != null)
            {
                AudioSource_Music.clip = clip;
                AudioSource_Music.Play();
            }
        }
        else
            Debug.LogError("Trying to play sound that does not exists! " + soundName);
    }
    AudioFile GetFileByName(string soundName)
    {
        var file = AudioFiles.First(x => x.Name == soundName);
        if (file != null)
        {
            return file;
        }
        return null;
    }
    public void PlaySFX(string soundName)
    {
        var file = GetFileByName(soundName);

        if(file != null)
        {
            var clip = file.Clip;
            AudioSource_SFX.volume = file.Volume * OverallVolume_SFX;
            AudioSource_SFX.clip = clip;
            AudioSource_SFX.Play();
        }
        else
            Debug.LogError("Trying to play sound that does not exists! " + soundName);
    }
    public void PlaySFX(string soundName, AudioSource source)
    {
        var file = GetFileByName(soundName);

        if (file != null)
        {
            var clip = file.Clip;
            source.volume = file.Volume * OverallVolume_SFX;
            source.clip = clip;
            source.Play();
        }
        else
            Debug.LogError("Trying to play sound that does not exists! " + soundName);
    }
    /*private void _PlaySFX(string name, AudioSource SFXSource)
    {
        var clip = GetFileByName(name);
        if (clip != null)
        {
            float vol = GlobalSFXVolume * clip.Volume;
            SFXSource.volume = vol;
            SFXSource.clip = clip.Clip;
            SFXSource.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                AudioMixer.FindMatchingGroups(name)[0] :
                AudioMixer.FindMatchingGroups("sfx")[0];

            SFXSource.Play();
        }
        else
            Debug.LogError("No such audio file " + name);
    }*/
}
