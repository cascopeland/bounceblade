using UnityEngine;

public class SkeletonNPC_Sound : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public AudioSource skeletonAudioSource; // Reference to the AudioSource component

    public float maxHearingDistance = 25f; // Maximum distance the skeleton can be heard

    private void Update()
    {
        // Calculate the distance between the skeleton and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Calculate the volume based on the distance
        float volume = Mathf.Clamp01(1.0f - distanceToPlayer / maxHearingDistance) * 0.3f;

        //Debug.Log("Skeleton Volume = " + volume);

        // Set the volume of the AudioSource
        skeletonAudioSource.volume = volume;
    }
}
