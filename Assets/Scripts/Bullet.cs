using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;


// Sound additions made by Jordan on 3-2-2024


public class Bullet : MonoBehaviour
{
    // Player GO
    GameObject player;

    // Bullet Sounds
    private string expirationSoundName = "ExpireBullet";    // Name of the expiration sound. TODO: Change Sound. 
    //private string fireSoundName = "FireBullet";            // Fire bullet
    //private string reloadSoundName = "";                    // Reloaded
    private string bounceHardSoundName = "HardBounce1";     // Hard Bounce
    private string bounceHardSoundName2 = "HardBounce2";    // Hard Bounce Alternate Sound
    private string bounceMediumSoundName = "MediumBounce";  // Medium Bounce 
    private string bounceSoftSoundName = "SoftBounce";      // Soft Bounce 
    public float maxDistance;                               // Maximum distance to hear a sound. Used in
    // Remember, Playsfx() takes two parameters: 'clip' and 'volume.'


    // Components
    private AudioManager audioManager;  // AudioManager object
    private Rigidbody rb;               // This character, the bullet.
    private Camera mainCamera;          // Main camera


    // Bullet Characteristics 
    public float bulletLifetime = 3f;
    private int bulletDamage;

    //gameManager
    GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioManager audioManager = FindObjectOfType<AudioManager>();    // Find AudioManager in the scene
        
        // Call DestroyBullet() after 2 seconds -- Audio Testing
        //Invoke("DestroyBullet", bulletLifetime);

        mainCamera = Camera.main;
        gameManager = GameManager.Instance;
        bulletDamage = gameManager.ballDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        /* Variables */
        /*  Audio */
        AudioManager audioManager = FindObjectOfType<AudioManager>();   // Find the AudioManager instance within the method body
        maxDistance = 50f;                                              // Set the maximum distance for sound calculation  

        /* Physics */
        ContactPoint contact = collision.contacts[0];                               // Get the contact point and 
        Vector3 reflectedVelocity = Vector3.Reflect(rb.velocity, contact.normal);   // Calculate the reflected velocity
        //rb.velocity = reflectedVelocity;                                            // Assign the reflected velocity to the Rigidbody's velocity
        
        /* Misc */
        player = GameObject.Find("FirePoint");                          // Used to calculate distance from player


        #region Bullet collision interaction with enemies
        EnemyCollider other = collision.gameObject.GetComponent<EnemyCollider>();
        if (other)
        {
            other.setHP(other.getHP() - bulletDamage);
            DestroyBullet();
            Debug.Log("Skeleton HP is " + other.getHP());

            if (other.getHP() <= 0)
            {
                other.Die();
            }
        }
        #endregion


        #region Play different sounds for each impact magnitude.

        float impactMagnitude = reflectedVelocity.magnitude; // Define magnitude
        float hardBounceCutoff = 15f;                        // Define threshold for sounds
        float mediumBounceCutoff = 2f;                       // Define threshold for sounds
        string bounceSound = "";

        // Play hardbounce sound. Random sound choice.
        if (audioManager != null)
        {
            if (impactMagnitude >= hardBounceCutoff)
            {
                // Randomly choose which hard bounce sound to play
                int randomIndex = Random.Range(0, 2);   // Generates a random integer between 0 (inclusive) and 2 (exclusive). i.e. 0 or 1.

                if (randomIndex == 0)
                {
                    bounceSound = bounceHardSoundName;      // Main hard bounce sound
                }
                else
                {
                    bounceSound = bounceHardSoundName2;     // Alternative hard bounce sound
                }
            }
            // Play medium bounce sound.
            if (impactMagnitude < hardBounceCutoff && impactMagnitude > mediumBounceCutoff)
            {
                bounceSound = bounceMediumSoundName;    // Main medium bounce sound
            }

            // Play soft bounce sound.
            if (impactMagnitude < mediumBounceCutoff)
            {
                bounceSound = bounceSoftSoundName;
            }

        #endregion Play different sounds for each impact magnitude.


        #region Simulate Volume from distance


        // Play sound when the bullet interacts with a surface (i.e., when a collision happens)

        Vector3 playerPosition = player.transform.position;


        // Calculate the distance between the character and the collision point
        float distance = Vector3.Distance(playerPosition, contact.point);

        // Calculate realistic volume. Volume decreases at sqrt of distance.
        float volume = Mathf.Clamp01(1f - Mathf.Sqrt(distance) / Mathf.Sqrt(maxDistance));

        #region Square Root of Distance Formula & Explanation
        /* 
            Square Root Relationship:
            volume = maxVolume * sqrt(1 - (distance / maxDistance))
            The volume decreases with the square root of the distance. 
            maxVolume is the initial volume when the distance is 0
            maxDistance is the distance at which the volume becomes 0.
        */
        #endregion


        // Play sound with adjusted volume
        audioManager.playSFX_Vol(bounceSound, volume);

        #endregion Simulate Volume from distance


        #region Debugging
        /* Volume */
        //Debug.Log("Reflected Velocity is: " + reflectedVelocity);
        //Debug.Log("Ball Velocity is: " + rb.velocity);
        //Debug.Log("Volume is: " + volume);      // Debug sounds.
        //Debug.Log("Sound is: " + bounceSound);
        /* Sound Choice */
        //Debug.Log("Impact Magnitude is: " + impactMagnitude);
        #endregion Debugging 

            
        }
    }




    public void DestroyBullet()     // Referenced by FireBullet()
    {
        if (audioManager != null && !string.IsNullOrEmpty(expirationSoundName))     // When audioManager and soundname is valid. 
        {
            audioManager.playSFX_Vol(expirationSoundName, .5f); // Play the expiration sound
        }
        Destroy(gameObject); // Destroy the bullet object
    }
}
