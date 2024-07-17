using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public AudioManager audioManager;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    public int playerDamage = 5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseScript.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

    }

    void Attack()
    {
        anim.SetTrigger("isSwinging");

        // swap between animations
        if (anim.GetBool("attack1"))
        {
            anim.SetBool("attack1", false);
            anim.SetBool("attack2", true);
        }
        else if (anim.GetBool("attack2"))
        {
            anim.SetBool("attack2", false);
            anim.SetBool("attack3", true);
        }
        else if (anim.GetBool("attack3"))
        {
            anim.SetBool("attack3", false);
            anim.SetBool("attack1", true);
        }
        audioManager.SwingSword();

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.gameObject.name.Contains("skeleton"))
            {
                enemy.GetComponent<SkeletonNPC_Combat>().TakeDamage(playerDamage);
            }
            else if (enemy.gameObject.name.Contains("slime"))
            {
                enemy.GetComponent<SlimeNPC_Combat>().TakeDamage(playerDamage);
            }
        }

        if (hitEnemies.Length != 0)
        {
            audioManager.playSFX("SwordDamage1");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
