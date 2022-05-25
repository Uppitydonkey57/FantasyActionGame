using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private MusicManager manager;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        float time = manager.GetSong(clip);
        Debug.LogError(time);
        source.time = time;
    }

    public void SaveMusic() 
    {
        manager.SaveSong(clip, source);
    }
}
