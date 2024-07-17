using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public GameObject slowBulletPrefab; //Reference to the slow bullet prefab
    public Transform firePoint; // The position from which the bullet will be fired
    private GameObject currentBullet; // Reference to the currently active bullet
    public float bulletSpeed = 5f;
    public float bulletLifeTime = 5f;   // Time bullet is active in-game. ADDITION.

    // Audio Management //
    private AudioManager audioManager; // Reference to the AudioManager script

    public Camera mainCamera;
    public Animator anim;

    public float movementSpeed = 10f;

    //health 
    private int playerHealth;

    //gameManager
    GameManager gameManager;

    //access for restartLevel
    RestartLevel restart;

    void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = FindObjectOfType<AudioManager>();
        mainCamera = FindObjectOfType<Camera>();
        playerHealth = gameManager.playerHealth;
        restart = new RestartLevel();
    }

    private void Update()
    {
        if (!PauseScript.isPaused)
        {
            //temporary input to fire slow bullet
            if (UnityEngine.Input.GetKeyDown(KeyCode.F) && currentBullet == null)
            {
                FireSlowBullet();
            }
            else if (UnityEngine.Input.GetKeyUp(KeyCode.Space) && currentBullet == null)
            {
                FireBullet();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = new(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical"));
        //Debug.DrawRay(transform.position + Vector3.up, movementVector, Color.blue, Time.deltaTime);
        float xAxisMovement = 0;
        float zAxisMovement = 0;
        var speed = movementSpeed * Time.deltaTime;
        movementVector = Quaternion.AngleAxis(mainCamera.transform.rotation.eulerAngles.y, Vector3.up) * movementVector;
        //movementVector = Quaternion.Euler(0, mainCamera.gameObject.transform.rotation.y, 0) * movementVector;
        //Debug.DrawRay(transform.position + Vector3.up * 0.9f, movementVector, Color.red, Time.deltaTime);

        // Find where our mouse is pointing in game
        Ray cameraRay = mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);

        //Debug.Log(UnityEngine.Input.mousePosition);

        // Make a plane from the ground
        Plane groundPlane = new(Vector3.up, Vector3.zero);


        // Check if when we cast a ray from the camera, it hits the ground
        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            // Find the intersection point of the camera ray and the ground
            Vector3 groundCameraRayIntersection = cameraRay.GetPoint(rayLength);

            Debug.DrawLine(cameraRay.origin, groundCameraRayIntersection, Color.yellow);

            // Remove the y-axis point, use the player's y-axis so we don't look up and down
            Vector3 lookAtPoint = new(groundCameraRayIntersection.x, transform.position.y, groundCameraRayIntersection.z);

            // Look at the point
            transform.LookAt(lookAtPoint);

            if (movementVector.magnitude > 0)
            {
                Vector3 direction = (lookAtPoint - transform.position).normalized;

                // Clamp the math here to keep it contained to the movement parameters in the blendtree
                zAxisMovement = Mathf.Clamp(Vector3.Dot(movementVector, direction), -1, 1);

                Vector3 horizontalLookingAt = new(direction.z, 0, -direction.x);
                xAxisMovement = Mathf.Clamp(Vector3.Dot(movementVector, horizontalLookingAt), -1, 1);

                anim.SetFloat("zAxisMovement", zAxisMovement);
                anim.SetFloat("xAxisMovement", xAxisMovement);
            } else
            {
                anim.SetFloat("zAxisMovement", 0);
                anim.SetFloat("xAxisMovement", 0);
            }


            // Don't forget to move the character DUH
            transform.position = transform.position + movementVector * speed;
        }
        else
        {
            transform.position = transform.position;
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
            anim.SetFloat("zAxisMovement", 0);
            anim.SetFloat("xAxisMovement", 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            gameManager.skillPoints = gameManager.skillPoints + 1;
        }

        if (other.tag == "LevelExit")
        {
            switch (other.gameObject.name)
            {
                case "WitchesMoatPortal":
                    SceneManager.LoadScene("Puzzle #1 - Witches Moat");
                    break;
                case "GraveyardPortal":
                    SceneManager.LoadScene("Puzzle #2");
                    break;
                case "HubAreaPortal":
                    SceneManager.LoadScene("Hub_Area");
                    break;
                case "WitchesMoatExit":
                    SceneManager.LoadScene("Hub_Area");
                    break;
                case "GraveyardExit":
                    SceneManager.LoadScene("Hub_Area");
                    break;
                case "FlyingLevelExit":
                    SceneManager.LoadScene("Hub_Area");
                    break;
                case "FlyingPuzzlePortal":
                    SceneManager.LoadScene("Flying Puzzle");
                    break;
            }
        }
    }


    void FireBullet()
    {
        if (skillTreeScript.isSkillUnlocked(skillTreeScript.skills.Ball))
        {
            currentBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;

            // Play "FireBullet" sound
            if (audioManager != null)
            {
                audioManager.playSFX("FireBullet");
            }

            // Call DestroyBullet() after bulletLifeTime seconds
            Invoke(nameof(DestroyBullet), bulletLifeTime);
        } else
        {
            Debug.Log("Skill not unlocked!");
        }


    }

    //slow bullet
    void FireSlowBullet()
    {
        if (skillTreeScript.isSkillUnlocked(skillTreeScript.skills.SlowBall))
        {
            currentBullet = Instantiate(slowBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;

            float bulletspeed = rb.velocity.magnitude;

            // Play "FireBullet" sound
            if (audioManager != null)
            {
                audioManager.playSFX("FireBullet");
            }

            // Call DestroyBullet() after bulletLifeTime seconds
            Invoke(nameof(DestroyBullet), bulletLifeTime);
        } else
        {
            Debug.Log("Skill not unlocked!");
        }

    }
    
    public void DestroyBullet()         // Referenced by FireBullet() via Invoke().
    {
        audioManager.DestroyBullet();   // Sound effect to destroy bullet

        Destroy(currentBullet);         // Destroy the bullet object
    }

    //collision with enemies reduces HP
    private void OnCollisionEnter(Collision collision)
    {
        EnemyCollider other = collision.gameObject.GetComponent<EnemyCollider>();
        
        //GET ROCK COMPONENT AND CHECK IF PLAYER COLLIDES WITH ROCK
        if (other)
        {
            playerHealth--;
            Debug.Log("Player health is " + playerHealth);
            if (playerHealth <= 0)
            {
                Debug.Log("Player health too low");
                restart.Restart();
            }
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            playerHealth = playerHealth - 3;
            Debug.Log("Player health is " + playerHealth);
            if (playerHealth <= 0)
            {
                Debug.Log("Player health too low");
                restart.Restart();
            }
        }
    }

}
