
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[Serializable]
public class Sound
{
    public string name;
    public AudioTypes audioType;
    public SoundType type;
    public List<SoundClip> clipList;

    [Range(0, 1f)]
    public float volume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;
    public bool loop = false;
    private AudioSource lastSource;
    private List<AudioSource> sourceList = new List<AudioSource>();


    public void CreateAudioSources(GameObject parent)
    {
        foreach (SoundClip soundClip in clipList)
        {
            AudioSource source = parent.AddComponent<AudioSource>();
            source.clip = soundClip.clip;
            source.outputAudioMixerGroup = soundClip.group;
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
            source.playOnAwake = loop;
            sourceList.Add(source);
        }
    }

    public void Play()
    {
        AudioSource currentClip = GetRandomClip();
        currentClip.Play();
    }

    public void Stop()
    {
        AudioSource currentClip = sourceList.First();
        currentClip.Stop();
    }

    public void ChangeVolume(float volume)
    {
        sourceList.ForEach(s => s.volume = volume);
    }

    public void Mute(bool isMuted)
    {
        sourceList.ForEach(s => s.mute = isMuted);
    }

    private AudioSource GetRandomClip()
    {
        if (sourceList.Count == 1) return sourceList.First();
        List<AudioSource> filterSources = sourceList.Except(new[] { lastSource }).ToList();
        lastSource = filterSources.ElementAt(Random.Range(0, filterSources.Count));
        return lastSource;
    }
}