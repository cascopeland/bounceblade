using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private Animator anim;
    private AnimationEvent animationEvent;
    
    // Audio
    public AudioManager audioManager;   // Death Sounds
    private Transform playerTransform;  // Distance Based Audio 
    private float maxHearingDistance;   // Max distance player can hear skeleton.
    private GameObject player;
    private GameObject npc;
    private UnityEngine.Vector3 playerPosition;
    private float volume;

    private List<string> animationNames = new List<string>() { "Attack 1", "Attack 2", "Attack 3" };

    private bool isDead = false;

    private float duration = 0;
    private int health;

    private bool shouldPlaySound = true;

    private GameManager gameManager;
    private Bullet bullet;

    private int swordDamage;
    private int ballDamage;
    private BoxCollider colliderComponent;

    private void Start()
    {
        anim = GetComponent<Animator>();
        animationEvent = GetComponent<AnimationEvent>();
        colliderComponent = GetComponent<BoxCollider>();
        // Audio Management //
        audioManager = FindObjectOfType<AudioManager>();

        //audioManager.playSFX("SkeletonRattle");

        //grabs a copy of gameManager
        gameManager = GameManager.Instance;
        //skeleton = new Enemy(28);
        //slime = new Enemy(1);
    }
    private void Update()
    {
        // destroy when animation is over
        if (isDead)
        {
            colliderComponent.enabled = false;
            duration += Time.deltaTime;
            //Destroy(gameObject);
        }
        if (duration > 2)
        {
            Destroy(gameObject);
            
        }


        #region Positional Audio

        // Entity Idle Sounds
        //StartCoroutine(PlayRattlingSound());

        /* Location of Player */
        player = GameObject.Find("FirePoint"); ;        // Detects the Player's FirePoint. Used for positional audio.
        playerPosition = player.transform.position;     // Location of character's firepoint.
        maxHearingDistance = 10f;                       // Adjust to taste. Maximum distance player can hear skeleton, will impact sound interpolation.

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = UnityEngine.Vector3.Distance(transform.position, playerPosition);

        // Calculate the volume based on the distance
        float volume = Mathf.Clamp01(1.0f - distanceToPlayer / maxHearingDistance);

        // Set the volume for the skeleton rattle sound
        //audioManager.playSFX_Vol("SkeletonRattle", volume);
        
        //Debug.Log("Skeleton Rattle Volume Dynamic: " + volume);
        //Debug.Log("Player Position is: " + playerPosition);

        #endregion End Positional Audio

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackArea"))
        {
            //check if animator is animating
            var playerAnim = other.GetComponentInParent<Animator>();
            foreach (string animName in animationNames)
            {
                if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName(animName))
                {
                    if (health > 0)
                    {
                        swordDamage = gameManager.swordDamage;
                        health -= swordDamage;

                        Debug.Log("hit");
                        Debug.Log("Current damage is " + swordDamage);
                    } else
                    {
                        Die();
                        Debug.Log("count");
                        break;
                    }
                    
                }
            }
            // todo: add some sound + enemy death animation
            // Death sound added.
            // maybe make enemy take more than one hit to kill
        }

    }

    public void Die()
    {
        // play death animation
        anim.SetTrigger("Dying");
        isDead = true;
        shouldPlaySound = false;        // Stop playing the rattle sound immediately


        if (audioManager != null)
        {
            audioManager.HitEnemy();    // Assuming Death() is a method in your AudioManager script
        }
        else
        {
            Debug.LogWarning("AudioManager is not assigned!");
        }

    }



    // Loop the entities idling sounds indefinitely until it dies.
    private IEnumerator PlayRattlingSound()
    {
        // Loop indefinitely until the enemy dies or shouldPlaySound flag becomes false
        while (!isDead && shouldPlaySound)
        {
            // Play rattling sound with calculated volume
            if (audioManager != null)
            {
                audioManager.playSFX_Vol("SkeletonRattle",volume); // Pass the volume variable here

                Debug.Log("Coroutine is running");

            }

            // Wait for a short duration before playing the sound again
            yield return new WaitForSeconds(5.0f); // Adjust the duration to length of sound clip. TODO: Match exactly with sound clip duration.
        }
    }

    //getter and setter for HP for outside interactions
    public void setHP(int newHP)
    {
        health = newHP;
    }

    public int getHP()
    {
        return health;
    }
}
