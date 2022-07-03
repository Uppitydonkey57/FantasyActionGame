using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

[CreateAssetMenu(fileName = "MusicManager", menuName = "Managers/MusicManager", order = 0)]
public class MusicManager : ScriptableObject 
{
    Dictionary<string, float> songs = new Dictionary<string, float>();
    
    public float GetSong(AudioClip clip) 
    {
        if (songs == null) return 0;

        if (songs.ContainsKey(clip.name))
        {
            Debug.Log("clip found!");
            return songs[clip.name];
        }  

        return 0; 
    }

    public void SaveSong(AudioClip clip, AudioSource source) 
    {
        Debug.LogError("SAVING");
        if (clip == null || source == null) return;

        if (!songs.ContainsKey(clip.name))
        {
            songs.Add(clip.name, source.time);
            Debug.LogError(clip.name);
        } 
        else 
        {
            songs[clip.name] = source.time;
            Debug.LogError("SAVING3");
        }
    }

    public void ResetSongs() 
    {
        songs = new Dictionary<string, float>();
    }

}
