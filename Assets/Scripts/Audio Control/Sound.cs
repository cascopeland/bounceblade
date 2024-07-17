//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound              //: MonoBehaviour       
{
    
    // Name of the sound
    public string name;

    // Audio clip associated with the sound
    public AudioClip clip;

    // Audio source to play the sound
    [HideInInspector]
    public AudioSource source;



    // Volume of the sound (ranged between 0 and 1)
    [Range(0f, 1f)]
    public float volume = .8f;

    // Pitch of the sound (ranged between 0.1 and 3)
    [Range(.1f, 3f)]
    public float pitch = 1;

}
