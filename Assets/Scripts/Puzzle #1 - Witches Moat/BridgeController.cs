using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected.");

        // Get the Animator component from the same GameObject
        Animator animator = GetComponent<Animator>();

        // Check if the Animator component exists
        if (animator != null)
        {
            Debug.Log("Animator component found.");

            // Set the "BridgeRaised" parameter to 1
            animator.SetInteger("BridgeRaised", 1);

            Debug.Log("BridgeRaised parameter set to 1.");
        }
        else
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
    }
} 
