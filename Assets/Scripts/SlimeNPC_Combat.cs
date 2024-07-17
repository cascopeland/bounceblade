using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNPC_Combat : MonoBehaviour
{
    #region VARIABLES
    public int maxHealth = 5;
    int currentHealth;
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

    void Die()
    {

        GetComponent<Collider>().enabled = false;
        this.enabled = false;
        Destroy(this.gameObject);
    }
}
