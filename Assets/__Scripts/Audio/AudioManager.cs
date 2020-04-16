using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    // member variables
    public Sound[] sounds;
    private int currentScene;

    // == methods ==
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            // set values for each sound object
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() 
    {
        // change music depending on scene
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 0 || currentScene == 5)
        {
            Play("MenuMusic");
        }
        else if(currentScene == 4)
        {
            Play("finalBattle");
        }
        else
        {
            Play("MissionMusic");
        } 
    }
    
    public void Play(string name)
    {
        // find audio file
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("SoundFX: " + s + " Not Found");
            return;
        }
        // play audio file
        s.source.Play();
    }

    public void OnValueChanged(float value)
    {
        // change volume of menu music
        Sound s1 = Array.Find(sounds, sound => sound.name == "MenuMusic");
        s1.source.volume = value;

        //change volume of mission music
        Sound s2 = Array.Find(sounds, sound => sound.name == "MissionMusic");
        s2.source.volume = value;

        //change volume of final battle music
        Sound s3 = Array.Find(sounds, sound => sound.name == "finalBattle");
        s3.source.volume = value;
    }

    public void OnValueChanged(Boolean value)
    {
        // apply toggle value to each audio clip
        foreach(Sound s in sounds)
        {
            if(value == true)
            {
                // un-mute
                AudioListener.pause = false; 
            }
            else
            {
                // mute
                AudioListener.pause = true;  
            }
        }
    }
}
