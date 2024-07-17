//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Buffers.Text;
using Unity.VisualScripting;
//using Unity.VisualScripting;

public class AudioManager : MonoBehaviour      
{

    public Sound[] sfxsounds;       //Changed from 'sounds' to sfxsounds
    public Sound[] music;
    //public AudioSource musicSource, sfxSource;


    /* ----------------------------------------------/ Initialize Sound Components and Options /----------------------------------------------*/

    void Awake()
    {
        foreach (Sound s in sfxsounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();      // Add componet for our sfx audio source
            s.source.clip = s.clip;

            s.source.volume = s.volume;     // Source sfx volume
            s.source.pitch = s.pitch;       // Source sfx pitch

        }

        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();      // Add componet for our music audio source
            s.source.clip = s.clip;

            s.source.volume = s.volume;     // Source music volume
            s.source.pitch = s.pitch;       // Source music pitch

        }
    }


    /* ----------------------------------------------/ Default Music & Ambiance /----------------------------------------------*/

    void Start()
    {
        PlayDefaultMusic();             // Play default music when the game starts.
        PlayDefaultAmbiance();          // Play environmental ambient music.
    }


    //
    void PlayDefaultMusic()
    {
        // Check if there is any default music defined
        if (music.Length > 0)
        {
            // Play the first music clip in the array
            playMusic(music[0].name, .4f);         // First sound in the music array for default music.
        }
        else
        {
            Debug.LogWarning("No music clips defined in AudioManager.");
        }
    }
    

    void PlayDefaultAmbiance()
    {
        // Check if there is any default music defined
        if (music.Length > 1) // Check if there are at least two elements in the music array
        {
            // Play the second music clip in the array with the name "Ambiance"
            Sound defaultAmbiance = Array.Find(music, sound => sound.name == "Ambiance");
            if (defaultAmbiance != null)
            {
                playMusic(defaultAmbiance.name); // Play the default ambiance sound
            }
            else
            {
                Debug.LogWarning("No ambiance clip found in the music array.");
            }
        }
        else
        {
            Debug.LogWarning("There must be at least two music clips defined in AudioManager to play default ambiance.");
        }
    }

    

    /* ----------------------------------------------/ Dynamic Music & Ambiance /----------------------------------------------*/


    public void playMusic(string name, float optionalVolume = .2f)
    {
        Sound s = Array.Find(music, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
            //s.source.volume = optionalVolume;       // Set the volume
            s.source.loop = true;
        }
        else
        {
            Debug.LogWarning("Music clip with name " + name + " not found.");
        }       //Error Handling
    }


    void PlayAmbiance(string name, float optionalVolume = 1.0f)
    {
        Sound s = Array.Find(music, sound => sound.name == name);

        if (s != null)
        {
            s.source.Play();
            //s.source.volume = optionalVolume;   // Set the volume
            s.source.loop = true;
        }
        else
        {
            Debug.LogWarning("Ambiance clip with name " + name + " not found.");
        }
    }





    /* ----------------------------------------------/ Sound Effects /----------------------------------------------*/

    // All Sound Effects

    //private string ExpireBullet = "ExpireBullet";     // Name of the expiration sound. TODO: Change Sound.
    //private string FireBullet = "FireBullet";         // Fire bullet
    //private string reloadSoundName = "";              // Reloaded
    private string HardBounce1  = "HardBounce1";        // Hard Bounce
    private string HardBounce2  = "HardBounce2";        // Hard Bounce Alternate Sound
    private string MediumBounce = "MediumBounce";       // Medium Bounce
    private string SoftBounce   = "SoftBounce";         // Soft Bounce
    private string SwingSword1 = "SwingSword1";         //
    private string SwingSword2  = "SwingSword2";        //
    private string SwordDamage1 = "SwordDamage1";       //
    private string SwordDamage2 = "SwordDamage2";       //
    //private string FootStep1    = "FootStep1";        //
    //private string FootStep2    = "FootStep2";        //
    private string SkeletonRattle = "SkeletonRattle";     //
    private string PuzzleBounce1 = "PuzzleBounce1";     // Ball Bounce sound when interacting w. a Puzzle Object 


    // Set Up SFX Player
    public void playSFX(string name)//, float optionalVolume = 1.0f)
    {
        Sound s = Array.Find(sfxsounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();                        // Play clip.
            //s.source.volume = optionalVolume;       // Set the volume
            s.source.loop = false;
        }
        else
        {
            Debug.LogWarning("Sound clip with name " + name + " not found.");
        }       //Error Handling
    }

    // Same as playSFX, but with a volume parameter.
    public void playSFX_Vol(string name, float optionalVolume = 0.1f)
    {
        Sound s = Array.Find(sfxsounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();                        // Play clip.
            s.source.volume = optionalVolume;       // Set the volume
            s.source.loop = false;
        }
        else
        {
            Debug.LogWarning("Sound clip with name " + name + " not found.");
        }       //Error Handling
    }


    /* --- Methods to be called below --- */


    #region Combat Sounds

    //Play designated SFX
    public void SwingSword()
    {
        // Array of sound names
        string[] soundNames = { SwingSword1, SwingSword2 };

        // Generate a random index within the range of the array length
        int randomIndex = UnityEngine.Random.Range(0, soundNames.Length);

        // Play the sound at the random index
        playSFX_Vol(soundNames[randomIndex], .3f);
    }

    public void HitEnemy()
    {
        // Array of sound names
        string[] soundNames = { SwordDamage1, SwordDamage2 };

        // Generate a random index within the range of the array length
        int randomIndex = UnityEngine.Random.Range(0, soundNames.Length);

        // Play the sound at the random index
        playSFX_Vol(soundNames[randomIndex], .55f);
    }

    #endregion



    #region Bounce Sounds

    public void PlayPuzzleBounce()
    {
        string[] soundNames = { PuzzleBounce1, PuzzleBounce1 };

        // Generate a random index within the range of the array length
        int randomIndex = UnityEngine.Random.Range(0, soundNames.Length);

        // Play the sound at the random index
        playSFX(soundNames[randomIndex]);//, .55f);
    }

    public void PlayHardBounce()
    {
        string[] soundNames = { HardBounce1, HardBounce2 };

        // Generate a random index within the range of the array length
        int randomIndex = UnityEngine.Random.Range(0, soundNames.Length);

        // Play the sound at the random index
        playSFX(soundNames[randomIndex]);//, .75f);
    }

    public void PlayMediumBounce()
    {
        string[] soundNames = { MediumBounce };

        // Generate a random index within the range of the array length
        int randomIndex = UnityEngine.Random.Range(0, soundNames.Length);

        // Play the sound at the random index
        playSFX(soundNames[randomIndex]);//, .75f);

    }

    public void PlaySoftBounce()
    {
        string[] soundNames = { SoftBounce };

        // Generate a random index within the range of the array length
        int randomIndex = UnityEngine.Random.Range(0, soundNames.Length);

        // Play the sound at the random index
        playSFX(soundNames[randomIndex]);//, .75f);

    }

    #endregion

    #region Bullet Sounds 


    #region Entities 
   
    public void PlaySkeletonRattle(float volume)
    {
        // Change skeleton entity sound based on if you are seen or not.
        //if (AIController.FollowTarget() == true)
        //{
        //
        //}
        playSFX_Vol(SkeletonRattle, volume);

    }

    private void Firebullet()
    {
        //
    }

    public void DestroyBullet()
    {

    }

    #endregion



    #endregion




}




