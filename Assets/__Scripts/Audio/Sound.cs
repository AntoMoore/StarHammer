using UnityEngine;

[System.Serializable]
public class Sound
{
    // define sound class
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(0f,1f)] public float volume;
    [Range(0f,1f)] public float pitch;
    [HideInInspector] public AudioSource source;
}
