using UnityEngine;

public class SlimeNPC_Sound : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public AudioSource slimeAudioSource; // Reference to the AudioSource component

    public float maxHearingDistance = 10f; // Maximum distance the skeleton can be heard
    public float maxVolume = 0.05f; // Maximum volume for the sound

    private void Update()
    {
        // Calculate the distance between the skeleton and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Calculate the volume based on the distance
        float volume = Mathf.Clamp01(1.0f - distanceToPlayer / maxHearingDistance) * maxVolume;

        // Set the volume of the AudioSource
        slimeAudioSource.volume = volume;
    }
}