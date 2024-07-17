using UnityEngine;
using UnityEngine.AI;


public class FootstepManagerSkeleton : MonoBehaviour
{
    /* Components */
    public AudioManager audioManager;               // Reference to the AudioManager script
    public NavMeshAgent navMeshAgent;               // Reference to the NavMeshAgent component

    /* Footfall Timings */
    public float minVelocityMagnitude = 0.1f;       // Minimum velocity magnitude to trigger footsteps
    private float footstepInterval = 0.18f;         // Initial interval between footstep sounds
    private float timer = 0f;                       // Timer to track elapsed time

    /* Volume Interpolation */
    public float maxVolume = 10f;
    public float peakVolume = 0.2f;                  // Maximum volume for footstep sound


    /* Footstep Sounds */
    private string footStep1 = "Footstep1";
    private string footStep2 = "Footstep2";
    private bool playFootstep1 = true;              // Index of the current footstep sound to play

    private void Update()
    {
        /* Calculate Speed */
        float playerSpeed = navMeshAgent.velocity.magnitude;     // Magnitude of the NavMeshAgent's velocity        

        Debug.Log("Skeleton Speed: " + navMeshAgent.velocity.magnitude);

        // Adjust footstep interval based on player speed
        footstepInterval = Mathf.Lerp(
            0.5f,               // Minimum footstep interval when the player is at rest
            0.17f,              // Maximum footstep interval when the player is moving at maximum speed
            Mathf.InverseLerp(  // Calculate the relative position of the player's speed between 0 and 10                   
                0f,             // Minimum speed (0) where the footstep interval is at its minimum                      
                3f,             // Maximum speed (10) where the footstep interval is at its maximum
                playerSpeed     // Current speed of the player
            )
        );


        // Check if the player's velocity magnitude is greater than the minimum threshold
        if (playerSpeed > minVelocityMagnitude)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if enough time has elapsed to play another footstep sound
            if (timer >= footstepInterval)
            {
                // Reset the timer
                timer = 0f;

                // Calculate volume based on the player's velocity magnitude
                float calcvolume = Mathf.Clamp01(playerSpeed / peakVolume);
                calcvolume = Mathf.Min(calcvolume, maxVolume); // Clamp the volume to the maximum allowed

                /* Footstep Sound Choice */
                // Play the footstep sound based on the current flag
                if (playFootstep1)
                {
                    audioManager.playSFX_Vol(footStep1, calcvolume);
                }
                else
                {
                    audioManager.playSFX_Vol(footStep2, calcvolume);
                }

                // Toggle the flag for the next footstep
                playFootstep1 = !playFootstep1;

                /* Speed and Volume Debug */
                Debug.Log("Player Speed is: " + navMeshAgent.velocity.magnitude);
                Debug.Log("Calculated Volume is: " + calcvolume);
            }
        }
        else
        {
            // Reset the timer if the player's velocity is below the threshold
            timer = 0f;
        }




    }




}