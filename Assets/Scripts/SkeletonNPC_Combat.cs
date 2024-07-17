using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonNPC_Combat : MonoBehaviour
{
    #region VARIABLES
    public int maxHealth = 15;
    int currentHealth;
    public Animator anim;
    public GameObject projectile;

    public float timeBetweenAttacks = 5f;
    bool alreadyAttacked;
    #endregion

    void Start()
    {
        currentHealth = maxHealth;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AttackPlayer(Transform player)
    {
        //Make sure enemy doesn't move
        //agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 3.2f, ForceMode.Impulse);
            rb.AddForce(transform.up * .6f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void Die()
    {
        anim.SetBool("isDead", true);

        GetComponent<Collider>().enabled = false;
        this.enabled = false;
        GetComponent<NPCAnimatorScript>().enabled = false;

        Destroy(this.gameObject, 2f);
    }
}
