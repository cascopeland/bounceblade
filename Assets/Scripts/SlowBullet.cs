using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Script is very similar to bullet script except decrease in vector3 reflect and destroy if the object's velocity gets too small
public class SlowBullet : MonoBehaviour
{

    //variables for implementation
    private Rigidbody rb;
    private int collisions = 0;
    private float collisionReduction;

    //Cleaner collision variables
    private float lastCollisionTime = -1;
    private bool hasCollided = false;

    int MAX_SLOWBULLETCOLLISIONS;

    //field for minimum ball speed
    [SerializeField]
    private float MIN_SLOWBULLETSPEED;

    //field for minimum time between speed reduction collisions
    [SerializeField]
    private float MIN_COLLISIONTIMEGAP;

    /*  Audio */
    private AudioManager audioManager;  // Find the AudioManager instance within the method body
    public float maxDistance = 50f;                                 // Set the maximum distance for sound calculation  

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        //AudioManager audioManager = FindObjectOfType<AudioManager>();    // Find AudioManager in the scene
        //check and update slowBall collisions if increased in skill tree;
        MAX_SLOWBULLETCOLLISIONS = gameManager.slowBallCollisons;
    }

    //the code is for hasCollided check is from https://stackoverflow.com/questions/40831775/unity-how-to-make-gameobject-make-only-one-collision-when-it-hits-two-collider
    //added a time component to ensure cleaner collisions
    private void OnCollisionEnter(Collision collision)
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        //checks if enough time has passed and if has collided in a previous cycle
        if (this.hasCollided == true && (Time.time - lastCollisionTime < MIN_COLLISIONTIMEGAP))
        {
            //Debug.Log("Time gap is " + (Time.time - lastCollisionTime));
            return;
        }
        this.hasCollided = true;
        ContactPoint contact = collision.contacts[0];

        //update collisions and then decrease speed by number of collisions over max
        collisions++;

        //telescopes collisions to reduce by 1/n for each factor (if 4 it would be 3/4 then 2/4 then 1/4 then 0)
        if (MAX_SLOWBULLETCOLLISIONS - collisions + 1 > 0) { 
            collisionReduction = (float)(MAX_SLOWBULLETCOLLISIONS - collisions) / (MAX_SLOWBULLETCOLLISIONS - collisions + 1);
        }   

        Vector3 reflectedVelocity = collisionReduction * Vector3.Reflect(rb.velocity, contact.normal);

        //Debug.Log("Bullet velocity is " + reflectedVelocity.magnitude);
        //Debug.Log("Collision number is " + collisions);
        if (reflectedVelocity.magnitude <= MIN_SLOWBULLETSPEED || collisions > MAX_SLOWBULLETCOLLISIONS )
        {
            Destroy(rb.gameObject);
        }
        //rb.velocity = reflectedVelocity;

        Puzzle_Ruins_Tutorial ruins = collision.gameObject.GetComponent<Puzzle_Ruins_Tutorial>();
        if (ruins != null)
        {
            ruins.ChangeColor();
        }

        Ruins moatRuins = collision.gameObject.GetComponent<Ruins>();
        if (moatRuins != null)
        {
            moatRuins.ChangeColor();
        }


        #region Slow Bullet collision interaction with enemies
        EnemyCollider other = collision.gameObject.GetComponent<EnemyCollider>();
        if (other)
        {
            Destroy(rb.gameObject);
        }
        #endregion

        #region AUDIO - Play different sounds for each impact magnitude.

        float impactMagnitude = reflectedVelocity.magnitude; // Define magnitude
        float hardBounceCutoff = 15f;                        // Define threshold for sounds
        float mediumBounceCutoff = 2f;                       // Define threshold for sounds

        // Play hardbounce sound. Random sound choice.
        if (audioManager != null)
        {

            // If material is a puzzle object. 
            if (collision.gameObject.CompareTag("PuzzleObject"))
            {
                audioManager.PlayPuzzleBounce();     // Alternative hard bounce sound
            }
               
        }
            
            // if (Material is environment)                 // Create different sounds for interacting w. Environment materials versus puzzle materials.
            else
            {

                if (impactMagnitude >= hardBounceCutoff)
                {
                    audioManager.PlayPuzzleBounce();     // Alternative hard bounce sound
                }

                // Play medium bounce sound.
                if (impactMagnitude < hardBounceCutoff && impactMagnitude > mediumBounceCutoff)
                {
                    audioManager.PlayMediumBounce();
                }

                // Play soft bounce sound.
                if (impactMagnitude < mediumBounceCutoff)
                {
                    audioManager.PlaySoftBounce();
                }
            }

            /* Debug */
            //Debug.Log("Red Ball - Impact Magnitude is: " + impactMagnitude);
            //Debug.Log("Red Ball - Velocity is: " + rb.velocity);

        
        #endregion Play different sounds for each impact magnitude.

    }
    //ensures one collision code from same as above

    private void LateUpdate()
    {
        this.hasCollided = false;
        lastCollisionTime = Time.time;
    }
}
